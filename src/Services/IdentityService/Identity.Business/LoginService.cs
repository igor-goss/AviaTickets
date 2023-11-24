using Identity.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Business
{
    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUser> _logger;

        public LoginService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ApplicationUser> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(string email,
            string password,
            bool rememberMe)
        {
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure:
                false);
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
    }
}
