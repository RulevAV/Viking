using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Viking.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> _userManager;

        public BaseController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Claim>> SetClaims(IdentityUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("idUser", user.Id));
            return claims;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
