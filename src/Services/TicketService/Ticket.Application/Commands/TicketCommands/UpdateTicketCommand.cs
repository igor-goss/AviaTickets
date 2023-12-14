using MediatR;
using Ticket.Application.DTO;

namespace Ticket.Application.Commands.TicketCommands
{
    public class UpdateTicketCommand : IRequest
    {
        public UpdateTicketDTO UpdateTicketDTO { get; set; }
    }
}
