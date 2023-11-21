using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Drawing.Text;

namespace IdentityServiceAPI
{
    public class SeedData
    {
        public static async Task Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var _roleManager = 
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _context = 
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await _context.Database.MigrateAsync();
            await _context.SaveChangesAsync();

            if (!await _roleManager.RoleExistsAsync("user"))
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }

        }
    }
}
