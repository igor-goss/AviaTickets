using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.QueryHandlers.AirportQueryHandlers;
using Ticket.Domain.Entities;

namespace TicketServiceAPI.Controllers.Queries
{
    [Route("api/airport")]
    [ApiController]
    public class AirportQueriesController : ControllerBase
    {
        private readonly GetAirportsQueryHandler _airportsQueryHandler;

        public AirportQueriesController(GetAirportsQueryHandler airportsQueryHandler)
        {
            _airportsQueryHandler = airportsQueryHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<Airport>> GetAirport(string? Name, string? Abbreviation, string? City, string? Country)
        {
            var result = await _airportsQueryHandler.Handle(Name, Abbreviation, City, Country);

            return result;
        }
    }
}
