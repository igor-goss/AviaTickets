using AutoMapper;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.TicketCommandHandlers
{
    public class CreateTicketCommandHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public CreateTicketCommandHandler(
            ITicketRepository ticketRepository,
            IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateTicketDTO createTicketDTO)
        {
            var command = _mapper.Map<CreateTicketCommand>(createTicketDTO);

            var existingAirport = await _ticketRepository.GetByTicketNumberAsync(command.TicketNumber);
            if (existingAirport != null)
            {
                throw new InvalidOperationException($"An airport with the number {command.TicketNumber} already exists.");
            }

            var ticket = _mapper.Map<Domain.Entities.Ticket>(command);

            await _ticketRepository.AddAsync(ticket);

            return;
        }
    }
}
