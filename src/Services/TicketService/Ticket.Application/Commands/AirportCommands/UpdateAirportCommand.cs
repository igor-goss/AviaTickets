using MediatR;
using Ticket.Application.DTO;

namespace Ticket.Application.Commands.AirportCommands
{
    public class UpdateAirportCommand : IRequest
    {
        public UpdateAirportDTO UpdateAirportDTO { get; set; }
    }
}
