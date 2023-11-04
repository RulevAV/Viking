
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Viking.Domain.Models.IdentityModels;
using Viking.Interfaces;
using Viking.Models.JWTModels;

namespace Viking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : Controller
{
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;
        private readonly ITokenService _tokenService;

        public AuthorizeController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> options, ITokenService tokenService)
        {
            _userManager = user;
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
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, loginDatas.login));

                await _userManager.AddClaimsAsync(user, claims);

                return Json(new { Flag = true });
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
            
            if(!result.Succeeded)
                return Json(new { Flag = false, Answer = "Доступ закрыт, проверьте правильность ввода пароля или зарегистрируйтесь" });
            
            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);

                _tokenService.AddClaims(user,claims);
                
                var token = _tokenService.GenerateAccessToken();
                var refreshToken = _tokenService.GenerateRefreshToken(Guid.Parse(user.Id));
                
                await _tokenService.AddRefreshTokensToBase(Guid.Parse(user.Id), refreshToken );
                
                return Json(new { Token = token, Flag = true, refreshToken = refreshToken });
            }

            return Json(new { Flag = false, Answer = "Нераспознанная ошибка доступа" });

        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        { 
            await _signInManager.SignOutAsync();
            
            return Json(null);

        }
        
        [HttpGet("AuthorizeCheck")]
        public JsonResult AuthorizeCheck()
        {
        
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { Flag = true });
            }
            else
            {
                return Json(new { Flag = false });
            }
        }

        [HttpGet("AuthCheck")]
        public JsonResult AuthCheck()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(false);
            }
            else
            {
                return Json(new { authorized = true, user = User.Identity.Name });
            }
        }

        [HttpPost("UpdateRefreshToken")]
        public async Task<IActionResult> UpdateRefreshToken([FromBody]string refreshToken)
        {
            var idUser = (await _tokenService.GetUserIdByRefreshToken(refreshToken)).UserId;
            await _tokenService.DeleteRefreshTokensToBase( idUser, refreshToken.ToString());
            var newRefeshToken = _tokenService.GenerateRefreshToken(idUser);
            var acessToken = _tokenService.GenerateAccessToken();
            await _tokenService.AddRefreshTokensToBase(idUser, newRefeshToken);
            var tokenApiModel = new TokenApiModel{RefreshToken = newRefeshToken.RefreshToken,AccessToken = acessToken};
            return Json(tokenApiModel);
        }
}