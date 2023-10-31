
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Viking.Models.IdentityModels;
using Viking.Services;
using Viking.Services.Repositories;

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

                var token = _tokenService.GenerateAccessToken(user, claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _tokenService.AddRefreshTokensToBase(Guid.Parse(user.Id), refreshToken );
                
                return Json(new { Token = token, Flag = true });
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
            if (User.Identity.IsAuthenticated == true)
            {
                return Json(false);
            }
            else
            {
                return Json(new { authorized = true, user = User.Identity.Name });
            }
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> principal)
        {
            var claims = principal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var Jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(Jwt);
        }
}