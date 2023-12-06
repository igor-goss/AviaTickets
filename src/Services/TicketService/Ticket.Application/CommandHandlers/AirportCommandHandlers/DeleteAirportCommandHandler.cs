using AutoMapper;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class DeleteAirportCommandHandler
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

        public async Task Handle(DeleteAirportDTO deleteAirportDTO)
        {
            var command = _mapper.Map<DeleteAirportCommand>(deleteAirportDTO);

            await _airportRepository.DeleteAsync(command.Id);

            return;
        }
    }
}
