using Microsoft.EntityFrameworkCore;
using Ticket.Persistence.Exceptions;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Persistence.Repositories.Implementations
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        private readonly AppDbContext _dbContext;

        public AirportRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Airport> GetByNameAsync(string airportName)
        {
            var result = await _dbContext.Airports.SingleOrDefaultAsync(a => a.Name == airportName);

            return result == null ? throw new EntityNotFoundException($"Airport with name {airportName} not found.") : result;
        }

        public IEnumerable<Airport> GetByCity(string city)
        {
            var result = _dbContext.Airports.Where(a => a.City == city);

            return result == null ? throw new EntityNotFoundException($"Airports in city {city} not found.") : result;
        }

        public IEnumerable<Airport> GetByCountry(string country)
        {
            var result = _dbContext.Airports.Where(a => a.Country == country);

            return result == null ? throw new EntityNotFoundException($"Airports in country {country} not found.") : result;
        }

        public async Task<Airport> GetByNameAbbreviationAsync(string abbreviation)
        {
            var result = await _dbContext.Airports.SingleOrDefaultAsync(a => a.Abbreviation == abbreviation);

            return result == null ? throw new EntityNotFoundException($"Airport with abbreviation {abbreviation} not found.") : result;
        }
    }
}
