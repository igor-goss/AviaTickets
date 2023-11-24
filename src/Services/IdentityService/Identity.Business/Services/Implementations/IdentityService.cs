using AutoMapper;
using Identity.Business.DTOs;
using Identity.Business.Services.Interfaces;
using Identity.Data.DTOs;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Business.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IMapper _mapper;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IdentityService> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterNewUserAsync(
            SignUpDTO signUpDTO)
        {
            if (signUpDTO.Password == null)
            {
                _logger.LogError("Password not provided");
                return null;
            }

            if (signUpDTO.Password != signUpDTO.ConfirmPassword)
            {
                _logger.LogError("The password and confirmation password do not match");
                return null;
            }

            var user = new ApplicationUser { UserName = signUpDTO.Email, Email = signUpDTO.Email };
            var result = await _userManager.CreateAsync(user, signUpDTO.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                loginDTO.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
            }
            else
            {
                _logger.LogError("Something wrong, but we didn't implement it yet :/");
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return;
        }

        public async Task<UserDTO> GetCurrentUserProfileAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            var result = _mapper.Map<UserDTO>(user);

            return result;
        }

        public async Task<UserDTO> EditCurrentUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserDTO updatedUser)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            user.CardNo = updatedUser.CardNo; //I don't know how to update only this fields, while keeping other untouched using AutoMapper
            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(
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
