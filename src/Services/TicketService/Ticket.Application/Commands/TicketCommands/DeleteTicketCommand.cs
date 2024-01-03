using MediatR;

namespace Ticket.Application.Commands.TicketCommands
{
    public class DeleteTicketCommand : IRequest
    {
        public int Id { get; set; }
    }
}
