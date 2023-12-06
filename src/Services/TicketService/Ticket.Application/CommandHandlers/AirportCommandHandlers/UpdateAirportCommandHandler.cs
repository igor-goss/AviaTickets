using AutoMapper;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class UpdateAirportCommandHandler
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public UpdateAirportCommandHandler(
            IAirportRepository airportRepository,
            IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAirportDTO updateAirportDTO)
        {
            var command = _mapper.Map<UpdateAirportDTO>(updateAirportDTO);

            var existingAirport = await _airportRepository.GetByNameAsync(command.Name);

            if (existingAirport == null)
            {
                throw new InvalidOperationException($"An airport with the name {command.Name} doesn't exists.");
            }

            var updatedAirport = _mapper.Map<Airport>(command);

            await _airportRepository.UpdateAsync(updatedAirport);
            return;
        }
    }
}
