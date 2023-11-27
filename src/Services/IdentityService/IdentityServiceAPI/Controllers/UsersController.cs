using Identity.Business.Services.Interfaces;
using Identity.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserProfileAsync()
        {
            return Ok(await _identityService.GetCurrentUserProfileAsync(this.User));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditCurrentUserAsync(UserDTO updatedUser)
        {
            return Ok(await _identityService.EditCurrentUserProfileAsync(this.User, updatedUser));
        }

        [HttpPut("password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            var result = await _identityService.ChangePasswordAsync(this.User, oldPassword, newPassword);

            return Ok(result);
        }
    }
}
