using Identity.Data;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceAPI
{
    public static class SeedData
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var _roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _context =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await _context.Database.MigrateAsync();
            await _context.SaveChangesAsync();

            var userRoleExists = await _roleManager.RoleExistsAsync(Roles.User.ToString());
            var adminRoleExists = await _roleManager.RoleExistsAsync(Roles.Admin.ToString());

            if (!userRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }

            if (!adminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
        }
    }
}
