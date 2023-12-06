using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;
using Ticket.Persistence;

namespace TicketServiceAPI.Extensions
{
    public static class SeedData
    {
        public static async Task Initialize(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (dbContext.Airports.Any() || dbContext.Tickets.Any())
            {
                // Data already seeded
                return;
            }

            await dbContext.Database.MigrateAsync();
            await dbContext.SaveChangesAsync();

            SeedAirports(dbContext);
            SeedTickets(dbContext);
        }

        private static void SeedAirports(AppDbContext dbContext)
        {
            var airports = new List<Airport>
        {
            new Airport { Name = "John F. Kennedy International Airport", City = "New York", Country = "USA", Abbreviation = "JFK" },
            new Airport { Name = "Los Angeles International Airport", City = "Los Angeles", Country = "USA", Abbreviation = "LAX" },
            new Airport { Name = "Heathrow Airport", City = "London", Country = "UK", Abbreviation = "LHR" },
            new Airport { Name = "Charles de Gaulle Airport", City = "Paris", Country = "France", Abbreviation = "CDG" },
            new Airport { Name = "Tokyo Haneda Airport", City = "Tokyo", Country = "Japan", Abbreviation = "HND" },
            new Airport { Name = "Beijing Capital International Airport", City = "Beijing", Country = "China", Abbreviation = "PEK" },
            new Airport { Name = "Dubai International Airport", City = "Dubai", Country = "UAE", Abbreviation = "DXB" },
            new Airport { Name = "Sydney Airport", City = "Sydney", Country = "Australia", Abbreviation = "SYD" },
            new Airport { Name = "Singapore Changi Airport", City = "Singapore", Country = "Singapore", Abbreviation = "SIN" },
            new Airport { Name = "Incheon International Airport", City = "Seoul", Country = "South Korea", Abbreviation = "ICN" },
            new Airport { Name = "Munich Airport", City = "Munich", Country = "Germany", Abbreviation = "MUC" },
            new Airport { Name = "Adolfo Suárez Madrid–Barajas Airport", City = "Madrid", Country = "Spain", Abbreviation = "MAD" },
            new Airport { Name = "Toronto Pearson International Airport", City = "Toronto", Country = "Canada", Abbreviation = "YYZ" },
            new Airport { Name = "Hong Kong International Airport", City = "Hong Kong", Country = "Hong Kong", Abbreviation = "HKG" },
            new Airport { Name = "Amsterdam Airport Schiphol", City = "Amsterdam", Country = "Netherlands", Abbreviation = "AMS" }
        };

            dbContext.Airports.AddRange(airports);
            dbContext.SaveChanges();
        }

        private static void SeedTickets(AppDbContext dbContext)
        {
            var random = new Random();

            var tickets = new List<Ticket.Domain.Entities.Ticket>();
            var airportIds = dbContext.Airports.Select(a => a.Id).ToList();

            for (int i = 0; i < 50; i++)
            {
                var fromAirportId = airportIds[random.Next(airportIds.Count)];
                var toAirportId = airportIds[random.Next(airportIds.Count)];

                while (fromAirportId == toAirportId)
                {
                    // Ensure different airports for origin and destination
                    toAirportId = airportIds[random.Next(airportIds.Count)];
                }

                var ticket = new Ticket.Domain.Entities.Ticket
                {
                    Price = (decimal)random.NextDouble() * 1000,
                    TicketNumber = i + 1,
                    DepartureDateTime = DateTime.Now.AddDays(random.Next(30)),
                    ArrivalDateTime = DateTime.Now.AddDays(random.Next(30, 60)),
                    FromAirportId = fromAirportId,
                    FromAirport = dbContext.Airports.Find(fromAirportId),
                    ToAirportId = toAirportId,
                    ToAirport = dbContext.Airports.Find(toAirportId)
                };

                tickets.Add(ticket);
            }

            dbContext.Tickets.AddRange(tickets);
            dbContext.SaveChanges();
        }
    }

}
