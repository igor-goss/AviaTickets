using Identity.Business.Exceptions;
using System.Reflection.Metadata;

namespace IdentityServiceAPI.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (InvalidCredentialsException ex)
            {
                _logger.LogError("Invalid username or password");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid username or password");
            }

            catch (RegistrationFailedException ex)
            {
                _logger.LogError($"Registrtion failed: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Registration failed.");
            }

            catch (PasswordChangeFailedException ex)
            {
                _logger.LogError($"Password change failed. {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Password change failed");
            }

            catch (UserNotFoundException ex)
            {
                _logger.LogError($"User not found. {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("User does not exist");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred");
            }
        }
    }
}
