using MediatR;
using Ticket.Domain.Entities;

namespace Ticket.Application.Queries.AirportQueries
{
    public class GetAirportsQuery : IRequest<IEnumerable<Airport>>
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Abbreviation { get; set; }
    }
}
