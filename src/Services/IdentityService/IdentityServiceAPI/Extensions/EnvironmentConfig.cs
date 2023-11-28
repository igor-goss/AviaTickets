using Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceAPI.Extensions
{
    public static class EnvironmentConfig
    {
        public static string EnvConfig(this WebApplicationBuilder builder) //have to finish 
        {
            var env = Environment.GetEnvironmentVariable("IS_RUNNING_IN_CONTAINER");

            string connectionString;

            if (env == "True")
            {
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Docker.json", optional: false);
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json", optional: false);
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            }
            return connectionString;
        }
    }
}
