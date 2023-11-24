using Duende.IdentityServer.Models;
using Identity.Data.Entities;
using Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityServiceAPI
{
    public static class IdentitySetup
    {
        public static void SetupIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddIdentityServer()
                            .AddInMemoryClients(new Client[] {
                    new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.Implicit,
                        RedirectUris = { "https://localhost:5002/signin-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                        FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                        AllowedScopes = { "openid", "profile", "email", "phone" }
                    }
                            })
                            .AddInMemoryIdentityResources(new IdentityResource[] {
                                new IdentityResources.OpenId(),
                                new IdentityResources.Profile(),
                                new IdentityResources.Email(),
                                new IdentityResources.Phone(),
                            })
                            .AddAspNetIdentity<ApplicationUser>();
        }
    }
}
