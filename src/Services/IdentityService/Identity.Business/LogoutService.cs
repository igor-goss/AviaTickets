using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Business
{
    public class LogoutService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutService> _logger;

        public LogoutService(SignInManager<ApplicationUser> signInManager, ILogger<LogoutService> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return;
        }
    }
}
