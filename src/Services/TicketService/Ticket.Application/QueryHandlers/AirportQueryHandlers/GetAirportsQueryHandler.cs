using MediatR;
using Ticket.Application.Queries.AirportQueries;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.AirportQueryHandlers
{
    public class GetAirportsQueryHandler : IRequestHandler<GetAirportsQuery, IEnumerable<Airport>>
    {
        private readonly IAirportRepository _airportRepository;

        public GetAirportsQueryHandler(
            IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<Airport>> Handle(GetAirportsQuery getAirportsQuery, CancellationToken cancellationToken)
        {
            var result = new List<Airport>();

            if (getAirportsQuery.Name != null)
            {
                var query = await _airportRepository.GetByNameAsync(getAirportsQuery.Name);
                result.Add(query);
            }

            if (getAirportsQuery.Abbreviation != null)
            {
                var query = await _airportRepository.GetByNameAbbreviationAsync(getAirportsQuery.Abbreviation);
                result.Add(query);
            }

            if (getAirportsQuery.City != null)
            {
                var query = _airportRepository.GetByCity(getAirportsQuery.City);
                result.AddRange(query);
            }

            if (getAirportsQuery.Country != null)
            {
                var query = _airportRepository.GetByCountry(getAirportsQuery.Country);
                result.AddRange(query);
            }

            return result;
        }
    }
}
