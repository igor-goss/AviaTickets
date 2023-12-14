using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Queries.TicketQueries;
using Ticket.Application.QueryHandlers.TicketQueryHandlers;

namespace TicketServiceAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("route")]
        public ActionResult<IEnumerable<Ticket.Domain.Entities.Ticket>> GetRoute(string originCity, string destinationCity)
        {
            return Ok(_mediator.Send(new GetRouteQuery()
            {
                OriginCity = originCity, DestinationCity = destinationCity
            }));
        }

    }
}
