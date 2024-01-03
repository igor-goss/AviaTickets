using Ticket.Application.CommandHandlers.TicketCommandHandlers;
using TicketServiceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDatabase();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureServices();

builder.Services.AddMediatR(opt =>
opt.RegisterServicesFromAssemblies(typeof(CreateTicketCommandHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.Initialize();

app.Run();
