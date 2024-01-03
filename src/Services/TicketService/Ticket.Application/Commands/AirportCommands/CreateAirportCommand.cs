using MediatR;
using Ticket.Application.DTO;

namespace Ticket.Application.Commands.AirportCommands
{
    public class CreateAirportCommand : IRequest
    {
        public CreateAirportDTO CreateAirportDTO { get; set; }
    }
}
