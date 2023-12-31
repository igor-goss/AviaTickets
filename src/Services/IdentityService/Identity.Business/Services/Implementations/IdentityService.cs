﻿using AutoMapper;
using FluentValidation;
using Identity.Business.DTOs;
using Identity.Business.Exceptions;
using Identity.Business.Services.Interfaces;
using Identity.Data.DTOs;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Identity.Business.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<PasswordChangeDTO> _validator;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<IdentityService> logger,
            IMapper mapper,
            IValidator<PasswordChangeDTO> validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IdentityResult> RegisterNewUserAsync(
            SignUpDTO signUpDTO)
        {
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

            if (!result.Succeeded)
            {
                throw new InvalidCredentialsException("Invalid username or password");
            }

            _logger.LogInformation("User logged in.");

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

            user = _mapper.Map<ApplicationUser>(user);

            _mapper.Map(updatedUser, user);

            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(
           ClaimsPrincipal claimsPrincipal,
           PasswordChangeDTO passwordChangeDTO)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            if (user == null)
            {
                throw new UserNotFoundException("User does not exist");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, passwordChangeDTO.OldPassword, passwordChangeDTO.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogError(error.Description);
                }

                throw new PasswordChangeFailedException("Password change failed");
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");

            return changePasswordResult;
        }
    }
}
