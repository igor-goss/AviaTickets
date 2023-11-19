using Duende.IdentityServer.Models;
using Identity.Business;
using Identity.Data;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
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

builder.Services.AddLogging(options =>
{
    options.AddFilter("Duende", LogLevel.Debug);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(options =>
{
    options.AddFilter("Duende", LogLevel.Debug);
});

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LogoutService>();
builder.Services.AddScoped<RegisterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
