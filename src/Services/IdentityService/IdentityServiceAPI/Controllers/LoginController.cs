using Identity.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Identity.Business; 

namespace IdentityServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly LoginService _loginService;

        public LoginController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ApplicationUser> logger,
            LoginService loginService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, 
            string password,
            bool rememberMe)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            /*var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: 
                false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
            }
            else
            {
                _logger.LogError("Something wromg, but we didn't implement it yet :/"); 
                return BadRequest(result);
            }
            return Ok(result);*/

            var result = await _loginService.Login(email, password, rememberMe);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return Ok(result);
            }
            else
            {
                _logger.LogError("Something wromg, but we didn't implement it yet :/");
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult UserStatus()
        {
            var result = _userManager.GetUserName(this.User);
            _logger.LogInformation($"Current Username: {result}");
            return Ok(result);
        }
    }
}
