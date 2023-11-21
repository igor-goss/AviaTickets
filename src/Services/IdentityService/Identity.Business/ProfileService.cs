using AutoMapper;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Data.DTOs;

namespace Identity.Business
{
    public class ProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ProfileService> _logger;
        private readonly IMapper _mapper;

        public ProfileService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ProfileService> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetCurrentUserProfile(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }

        public async Task<UserDTO> EditCurrentUserProfile(ClaimsPrincipal claimsPrincipal, UserDTO updatedUser)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            user.CardNo = updatedUser.CardNo;
            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber;


            var updateResult = await _userManager.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IdentityResult> ChangePassword(
            ClaimsPrincipal claimsPrincipal,
            string oldPassword,
            string newPassword)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogError(error.Description);
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");

            return changePasswordResult;
        }
    }
}
