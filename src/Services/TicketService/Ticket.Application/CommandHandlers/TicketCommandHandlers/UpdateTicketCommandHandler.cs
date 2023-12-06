using AutoMapper;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.TicketCommandHandlers
{
    public class UpdateTicketCommandHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public UpdateTicketCommandHandler(
            ITicketRepository ticketRepository,
            IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateTicketDTO updateTicketDTO)
        {
            var command = _mapper.Map<UpdateTicketCommand>(updateTicketDTO);

            var existingTicket = await _ticketRepository.GetByTicketNumberAsync(command.TicketNumber);

            if (existingTicket == null)
            {
                throw new InvalidOperationException($"A ticket with the number {command.TicketNumber} doesn't exists.");
            }

            var updatedTicket = _mapper.Map<Domain.Entities.Ticket>(command);

            await _ticketRepository.UpdateAsync(updatedTicket);
            return;
        }
    }
}
