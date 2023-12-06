using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.AirportQueryHandlers
{
    public class GetAirportsQueryHandler
    {
        private readonly IAirportRepository _airportRepository;

        public GetAirportsQueryHandler(
            IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<Airport>> Handle(string? Name, string? Abbreviation, string? City, string? Country)
        {
            var result = new List<Airport>();

            if (Name != null)
            {
                var query = await _airportRepository.GetByNameAsync(Name);
                result.Add(query);
            }

            if (Abbreviation != null)
            {
                var query = await _airportRepository.GetByNameAbbreviationAsync(Abbreviation);
                result.Add(query);
            }

            if (City != null)
            {
                var query = _airportRepository.GetByCity(City);
                result.AddRange(query);
            }

            if (Country != null)
            {
                var query = _airportRepository.GetByCountry(Country);
                result.AddRange(query);
            }

            return result;
        }
    }
}
