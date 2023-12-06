using AutoMapper;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class CreateAirportCommandHandler
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public CreateAirportCommandHandler(
            IAirportRepository airportRepository,
            IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateAirportDTO createAirportDTO)
        {
            var command = _mapper.Map<CreateAirportCommand>(createAirportDTO);


            var existingAirport = await _airportRepository.GetByNameAsync(command.Name);
            if (existingAirport != null)
            {
                throw new InvalidOperationException($"An airport with the name {command.Name} already exists.");
            }

            var airport = _mapper.Map<Airport>(command);

            await _airportRepository.AddAsync(airport);

            return;
        }
    }
}
