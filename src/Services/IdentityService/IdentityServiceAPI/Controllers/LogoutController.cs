using Identity.Business;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutController> _logger;
        private readonly LogoutService _logoutService;

        public LogoutController(SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutController> logger,
            LogoutService logoutService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _logoutService = logoutService;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _logoutService.Logout();
            return Ok();
        }
    }
}
