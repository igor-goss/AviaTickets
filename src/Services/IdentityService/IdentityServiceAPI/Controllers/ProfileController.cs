using Duende.IdentityServer.AspNetIdentity;
using Identity.Data.DTOs;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly Identity.Business.ProfileService _profileService;
        public ProfileController(Identity.Business.ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            return Ok(await _profileService.GetCurrentUserProfile(this.User));
        }

        [HttpPut]
        public async Task<IActionResult> EditCurrentUser(UserDTO updatedUser)
        {
            return Ok(await _profileService.EditCurrentUserProfile(this.User, updatedUser));
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            var result = await _profileService.ChangePassword(this.User, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
