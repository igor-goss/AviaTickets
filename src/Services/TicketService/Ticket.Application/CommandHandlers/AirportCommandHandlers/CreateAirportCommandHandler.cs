using AutoMapper;
using MediatR;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;
using Ticket.Persistence.Exceptions;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand>
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

        public async Task Handle(CreateAirportCommand command, CancellationToken cancellationToken)
        {
            Airport existingAirport = null;

            try
            {
                existingAirport = await _airportRepository.GetByNameAsync(command.CreateAirportDTO.Name);
            }
            catch (EntityNotFoundException ex)
            {

            }

            if (existingAirport != null)
            {
                throw new InvalidOperationException($"An airport with the name {command.CreateAirportDTO.Name} already exists.");
            }

            var airport = _mapper.Map<Airport>(command.CreateAirportDTO);

            await _airportRepository.AddAsync(airport);

            return;
        }
    }
}
