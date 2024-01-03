using Ticket.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TicketServiceAPI.Extensions
{
    public static class DBConfig
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                           options.UseSqlServer(connectionString));

        }
    }
}
