using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.CommandHandlers.TicketCommandHandlers;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Application.Queries.TicketQueries;
using Ticket.Application.QueryHandlers.TicketQueryHandlers;

namespace TicketServiceAPI.Controllers.Commands
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket.Domain.Entities.Ticket>>> GetTickets(int TicketNumber)
        {
            return Ok(await _mediator.Send(new GetTicketQuery() { TicketNumber = TicketNumber} ));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketDTO createTicketDTO)
        {
            await _mediator.Send(new CreateTicketCommand() { CreateTicketDTO = createTicketDTO });
            
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTicket(UpdateTicketDTO updateTicketDTO)
        {
            await _mediator.Send(new UpdateTicketCommand() { UpdateTicketDTO = updateTicketDTO });

            return Ok();
        }

        [HttpDelete]
        public async Task DeleteTicket(DeleteTicketDTO deleteTicketDTO)
        {
            await _mediator.Send(new DeleteTicketCommand() { Id = deleteTicketDTO.Id });
        }
    }
}
