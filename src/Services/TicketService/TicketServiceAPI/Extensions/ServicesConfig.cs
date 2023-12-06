using AutoMapper;
using Ticket.Persistence;
using Microsoft.EntityFrameworkCore;
using Ticket.Application.Mappers;
using Ticket.Persistence.Repositories.Interfaces;
using Ticket.Persistence.Repositories.Implementations;
using TicketServiceAPI.Extensions;
using Ticket.Application.QueryHandlers.TicketQueryHandlers;
using Ticket.Application.QueryHandlers.AirportQueryHandlers;
using Ticket.Application.CommandHandlers.AirportCommandHandlers;
using Ticket.Application.CommandHandlers.TicketCommandHandlers;

namespace TicketServiceAPI.Extensions
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                           options.UseSqlServer(connectionString));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var mapperConfig = new MapperConfiguration(options =>
            {
                options.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IAirportRepository, AirportRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            builder.Services.AddScoped<GetTicketQueryHandler>();
            builder.Services.AddScoped<GetRouteQueryHandler>();
            builder.Services.AddScoped<GetAirportsQueryHandler>();
            builder.Services.AddScoped<CreateAirportCommandHandler>();
            builder.Services.AddScoped<DeleteAirportCommandHandler>();
            builder.Services.AddScoped<UpdateAirportCommandHandler>();
            builder.Services.AddScoped<CreateTicketCommandHandler>();
            builder.Services.AddScoped<UpdateTicketCommandHandler>();
            builder.Services.AddScoped<DeleteTicketCommandHandler>();
        }

    }
}
