using AutoMapper;
using MediatR;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.AirportCommandHandlers
{
    public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand>
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

        public async Task Handle(UpdateAirportCommand updateAirportCommand, CancellationToken cancellationToken)
        {
            var existingAirport = await _airportRepository.GetByNameAsync(updateAirportCommand.UpdateAirportDTO.Name);

            if (existingAirport == null)
            {
                throw new InvalidOperationException($"An airport with the name {updateAirportCommand.UpdateAirportDTO.Name} doesn't exists.");
            }

            var updatedAirport = _mapper.Map<Airport>(updateAirportCommand.UpdateAirportDTO);

            await _airportRepository.UpdateAsync(updatedAirport);
        }
    }
}
