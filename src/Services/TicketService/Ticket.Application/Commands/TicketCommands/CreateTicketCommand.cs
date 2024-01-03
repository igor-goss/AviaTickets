using MediatR;
using Ticket.Application.DTO;

namespace Ticket.Application.Commands.TicketCommands
{
    public class CreateTicketCommand : IRequest
    {
        public CreateTicketDTO CreateTicketDTO { get; set; }
    }
}
