using Identity.Business.DTOs;
using Identity.Business.Services.Interfaces;
using Identity.Data.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUserAsync(SignUpDTO signUpDTO)
        {
            var result = await _identityService.RegisterNewUserAsync(signUpDTO);
            
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO) 
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var result = await _identityService.LoginAsync(loginDTO);

            return Ok(result);
        }

        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogoutAsync();

            return Ok();
        }
    }
}
