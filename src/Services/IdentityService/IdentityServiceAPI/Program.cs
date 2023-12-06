using AutoMapper;
using Identity.Business.Services.Implementations;
using Identity.Business.Services.Interfaces;
using Identity.Data;
using Identity.Data.Mapper;
using Microsoft.EntityFrameworkCore;
using IdentityServiceAPI.Middleware;
using IdentityServiceAPI;
using IdentityServiceAPI.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.ConfigureDatabaseConnection(); //choosing correct appsettings.json and getting connection string 

builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(connectionString));

builder.SetupIdentity(); //Extension method from IdentitySetup.cs

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(options =>
{
    options.AddFilter("Duende", LogLevel.Debug);
});

var mapperConfig = new MapperConfiguration(options =>
{
    options.AddProfile(new MappingProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Identity.Business.Validators.PasswordValidator>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

await app.SeedAsync();

app.Run();
