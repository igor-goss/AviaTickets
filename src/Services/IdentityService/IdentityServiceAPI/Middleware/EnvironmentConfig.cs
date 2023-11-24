using Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceAPI
{
    public static class EnvironmentConfig
    {
        public static string EnvConfig(this WebApplicationBuilder builder) //have to finish 
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            string connectionString;

            if (env == "DockerDevelopment")
            {
                connectionString = builder.Configuration.GetConnectionString("ContainerConnection");
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Docker.json", optional: false);
            }
            else
            {
                connectionString = builder.Configuration.GetConnectionString("LocalConnection");
                builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json", optional: false);
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            return connectionString;
        }
    }
}
