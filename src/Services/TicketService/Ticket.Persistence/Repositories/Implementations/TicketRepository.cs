using Microsoft.EntityFrameworkCore;
using Ticket.Persistence.Exceptions;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Persistence.Repositories.Implementations
{
    public class TicketRepository : Repository<Domain.Entities.Ticket>, ITicketRepository
    {
        private readonly AppDbContext _dbContext;

        public TicketRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Ticket> GetByTicketNumberAsync(int ticketNumber)
        {
            var result = await _dbContext.Tickets
                .Include(t => t.ToAirport)
                .Include(t => t.FromAirport)
                .SingleOrDefaultAsync(a => a.TicketNumber == ticketNumber);

            return result == null ? throw new EntityNotFoundException($"Ticket with number {ticketNumber} not found.") : result;
        }

        public IEnumerable<Domain.Entities.Ticket> GetByOrigin(string airportName)
        {
            var result = _dbContext.Tickets
                .Where(t => t.FromAirport.Name == airportName)
                .Include(t => t.FromAirport)
                .Include(t => t.ToAirport);

            return result == null ? throw new EntityNotFoundException($"Ticket with origin airport {airportName} not found.") : result;
        }

        public IEnumerable<Domain.Entities.Ticket> GetByDestianation(string airportName)
        {
            var result = _dbContext.Tickets
                .Where(t => t.ToAirport.Name == airportName)
                .Include(t => t.FromAirport)
                .Include(t => t.ToAirport);

            return result == null ? throw new EntityNotFoundException($"Ticket with destination airport {airportName} not found.") : result;
        }

        public IEnumerable<IEnumerable<Domain.Entities.Ticket>> FindRoutesBetweenCities(string originCity, string destinationCity)
        {
            var ticketsByCity = GetTicketsByCity();
            var visited = new HashSet<string>();
            var queue = new Queue<List<Domain.Entities.Ticket>>();

            foreach (var ticket in ticketsByCity.GetValueOrDefault(originCity, Enumerable.Empty<Domain.Entities.Ticket>()))
            {
                queue.Enqueue(new List<Domain.Entities.Ticket> { ticket });
            }

            while (queue.Any())
            {
                var currentRoute = queue.Dequeue();
                var lastTicket = currentRoute.Last();

                if (lastTicket.ToAirport.City == destinationCity)
                {
                    yield return currentRoute;
                    continue;
                }

                var layoverCity = lastTicket.ToAirport.City;

                if (visited.Contains(layoverCity))
                {
                    continue;
                }

                visited.Add(layoverCity);

                foreach (var nextTicket in ticketsByCity.GetValueOrDefault(layoverCity, Enumerable.Empty<Domain.Entities.Ticket>()))
                {
                    var newRoute = new List<Domain.Entities.Ticket>(currentRoute);
                    newRoute.Add(nextTicket);
                    queue.Enqueue(newRoute);
                }
            }
        }

        private Dictionary<string, IEnumerable<Domain.Entities.Ticket>> GetTicketsByCity()
        {
            var allTickets = _dbContext.Tickets
                .Include(t => t.FromAirport)
                .Include(t => t.ToAirport)
                .AsEnumerable();

            return allTickets
                .GroupBy(t => t.FromAirport.City)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
        }

    }
}
