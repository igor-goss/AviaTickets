using Identity.Business;
using Identity.Business.DTOs;
using Identity.Business.Services.Interfaces;
using Identity.Data.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(SignUpDTO signUpDTO)
        {
            var result = await _identityService.RegisterNewUserAsync(signUpDTO);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO) 
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var result = await _identityService.LoginAsync(loginDTO);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            return Ok(await _identityService.GetCurrentUserProfileAsync(this.User));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditCurrentUser(UserDTO updatedUser)
        {
            return Ok(await _identityService.EditCurrentUserProfileAsync(this.User, updatedUser));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var result = await _identityService.ChangePasswordAsync(this.User, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
