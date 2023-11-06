
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Viking.Domain.Models.IdentityModels;
using Viking.Interfaces;
using Viking.Models.JWTModels;
using Newtonsoft.Json.Linq;
using Viking.Models;
using Viking.Models.Sports;
using Viking.Repositories;

namespace Viking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : BaseController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JWTSettings _options;
    private readonly ITokenService _tokenService;

    public AuthorizeController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> options, ITokenService tokenService) : base(user)
    {
        _signInManager = signIn;
        _options = options.Value;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] ParamsUsers loginDatas)
    {
        var user = new IdentityUser { UserName = loginDatas.login };
        var result = await _userManager.CreateAsync(user, loginDatas.password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            var claims = await SetClaims(user);
            var token = _tokenService.GenerateAccessToken(claims);
            var newRefeshToken = _tokenService.GenerateRefreshToken(Guid.Parse(user.Id));
            var objRefreshToken = new RefreshToken
            {
                Token = newRefeshToken.RefreshToken,
                CreatedTime = newRefeshToken.RefreshTokenCreatedTime,
                ExpiryTime = newRefeshToken.RefreshTokenExpiryTime
            };
            return Json(new TokenApiModel { AccessToken = token, RefreshToken = objRefreshToken });

        }
        else
        {
            var Errors = result.Errors;
            return Json(new { Flag = false, Result = Errors });
        }
    }
    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] ParamsUsers loginDatas)
    {
        var user = await _userManager.FindByNameAsync(loginDatas.login);

        if (user == null)
            return Json(new { Flag = false, Answer = "Пользователь не найден, проверьте логин или зарегистрируйтесь" });

        var result = await _signInManager.PasswordSignInAsync(user, loginDatas.password, false, false);

        if (!result.Succeeded)
            return Json(new { Flag = false, Answer = "Доступ закрыт, проверьте правильность ввода пароля или зарегистрируйтесь" });

        if (result.Succeeded)
        {
            var claims = await SetClaims(user);
            var token = _tokenService.GenerateAccessToken(claims);
            var newRefeshToken = _tokenService.GenerateRefreshToken(Guid.Parse(user.Id));

            await _tokenService.AddRefreshTokensToBase(Guid.Parse(user.Id), newRefeshToken);

            var objRefreshToken = new RefreshToken
            {
                Token = newRefeshToken.RefreshToken,
                CreatedTime = newRefeshToken.RefreshTokenCreatedTime,
                ExpiryTime = newRefeshToken.RefreshTokenExpiryTime
            };

            return Json(new TokenApiModel { AccessToken = token, RefreshToken = objRefreshToken });
        }

        return Json(new { Flag = false, Answer = "Нераспознанная ошибка доступа" });

    }
    [HttpGet("AuthorizeCheck")]
    public JsonResult AuthorizeCheck()
    {
        return Json(User.Identity.IsAuthenticated);
    }
    [HttpPost("UpdateRefreshToken")]
    public async Task<IActionResult> UpdateRefreshToken([FromBody] TokenApiModel model)
    {
        var userRefreshToken = await _tokenService.GetUserIdByRefreshToken(model.RefreshToken.Token);
        if (userRefreshToken == null) { return BadRequest("Токен не найден"); };

        var user = await _userManager.FindByIdAsync(userRefreshToken.UserId.ToString());
        if (user == null) { return BadRequest("Пользователь не найден"); };

        var claims = await SetClaims(user);

        await _tokenService.DeleteRefreshTokensToBase(userRefreshToken.UserId, model.RefreshToken.Token);
        var newRefeshToken = _tokenService.GenerateRefreshToken(userRefreshToken.UserId);
        var acessToken = _tokenService.GenerateAccessToken(claims);
        await _tokenService.AddRefreshTokensToBase(userRefreshToken.UserId, newRefeshToken);

        var refreshToken = new RefreshToken
        {
            Token = newRefeshToken.RefreshToken,
            CreatedTime = newRefeshToken.RefreshTokenCreatedTime,
            ExpiryTime = newRefeshToken.RefreshTokenExpiryTime
        };
        return Json(new TokenApiModel { RefreshToken = refreshToken, AccessToken = acessToken });
    }
    [HttpGet("GetUser")]
    [Authorize]
    public JsonResult GetUser()
    {
        var Name = User.Claims.First(u => u.Type == ClaimTypes.Name).Value;
        return Json(Name);
    }

    [HttpPost("LogOut")]
    [Authorize]
    public async Task<IActionResult> LogOut([FromBody] TokenApiModel model)
    {
        var id = User.Claims.First(u => u.Type == "idUser").Value;
        var userId = Guid.Parse(id);
        await _tokenService.DeleteRefreshTokensToBase(userId, model.RefreshToken.Token);
        await _signInManager.SignOutAsync();
        return Json(null);
    }
}