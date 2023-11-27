using AutoMapper;
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
                throw new RegistrationFailedException("Password not provided");
            }

            if (signUpDTO.Password != signUpDTO.ConfirmPassword)
            {
                throw new RegistrationFailedException("Passwords don't match");
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
           string oldPassword,
           string newPassword)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            if (user == null)
            {
                return null;
            }

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
