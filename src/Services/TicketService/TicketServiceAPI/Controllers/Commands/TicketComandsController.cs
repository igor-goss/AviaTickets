using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.CommandHandlers.TicketCommandHandlers;
using Ticket.Application.DTO;

namespace TicketServiceAPI.Controllers.Commands
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketComandsController : ControllerBase
    {
        private readonly CreateTicketCommandHandler _createTicketCommandHandler;
        private readonly UpdateTicketCommandHandler _updateTicketCommandHandler;
        private readonly DeleteTicketCommandHandler _deleteTicketCommandHandler;

        public TicketComandsController(CreateTicketCommandHandler createTicketCommandHandler, 
            UpdateTicketCommandHandler updateTicketCommandHandler,
            DeleteTicketCommandHandler deleteTicketCommandHandler)
        {
            _createTicketCommandHandler = createTicketCommandHandler;
            _updateTicketCommandHandler = updateTicketCommandHandler;
            _deleteTicketCommandHandler = deleteTicketCommandHandler;
        }

        [HttpPost]
        public async Task CreateTicket(CreateTicketDTO createTicketDTO)
        {
            await _createTicketCommandHandler.Handle(createTicketDTO);
        }

        [HttpPatch]
        public async Task UpdateTicket(UpdateTicketDTO updateTicketDTO)
        {
            await _updateTicketCommandHandler.Handle(updateTicketDTO);
        }

        [HttpDelete]
        public async Task DeleteTicket(DeleteTicketDTO deleteTicketDTO)
        {
            await _deleteTicketCommandHandler.Handle(deleteTicketDTO);
        }
    }
}
