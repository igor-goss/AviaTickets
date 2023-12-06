using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.DTO;
using Ticket.Application.Queries.TicketQueries;
using Ticket.Application.QueryHandlers.TicketQueryHandlers;

namespace TicketServiceAPI.Controllers.Queries
{
    [Route("api")]
    [ApiController]
    public class TicketQueriesController : ControllerBase
    {
        private readonly GetTicketQueryHandler _getTicketCommandHandler;
        private readonly GetRouteQueryHandler _getRouteCommandHandler;

        public TicketQueriesController(
            GetTicketQueryHandler getTicketCommandHandler,
            GetRouteQueryHandler getRouteCommandHandler)
        {
            _getTicketCommandHandler = getTicketCommandHandler;
            _getRouteCommandHandler = getRouteCommandHandler;
        }

        [HttpGet("ticket")]
        public async Task<ActionResult<IEnumerable<Ticket.Domain.Entities.Ticket>>> GetTickets(int TicketNumber)
        {
            return Ok(await _getTicketCommandHandler.Handle(TicketNumber));
        }

        [HttpGet("route")]
        public ActionResult<IEnumerable<Ticket.Domain.Entities.Ticket>> GetRoute(string originCity, string destinationCity)
        {
            return Ok(_getRouteCommandHandler.Handle(originCity, destinationCity));
        }

    }
}
