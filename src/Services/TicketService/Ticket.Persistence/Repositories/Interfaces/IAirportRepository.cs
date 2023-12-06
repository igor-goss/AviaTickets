using Ticket.Domain.Entities;

namespace Ticket.Persistence.Repositories.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        public Task<Airport> GetByNameAsync(string airportName);
        public IEnumerable<Airport> GetByCity(string city);
        public IEnumerable<Airport> GetByCountry(string country);
        public Task<Airport> GetByNameAbbreviationAsync(string abbreviation);
    }
}
