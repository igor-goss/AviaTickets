using MediatR;

namespace Ticket.Application.Commands.AirportCommands
{
    public class DeleteAirportCommand : IRequest
    {
        public int Id { get; set; }
    }
}
