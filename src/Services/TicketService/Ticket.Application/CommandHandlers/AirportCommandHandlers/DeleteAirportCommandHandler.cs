using AutoMapper;
using MediatR;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand>
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public DeleteAirportCommandHandler(
            IAirportRepository airportRepository,
            IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAirportCommand deleteAirportCommand, CancellationToken cancellationToken)
        {
            await _airportRepository.DeleteAsync(deleteAirportCommand.Id);
        }
    }
}
