using AutoMapper;
using MediatR;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.TicketCommandHandlers
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
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

        public async Task Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
        {
            var existingTicket = await _ticketRepository.GetByTicketNumberAsync(command.UpdateTicketDTO.TicketNumber);

            if (existingTicket == null)
            {
                throw new InvalidOperationException($"A ticket with the number {command.UpdateTicketDTO.TicketNumber} doesn't exists.");
            }

            var updatedTicket = _mapper.Map<Domain.Entities.Ticket>(command.UpdateTicketDTO);

            await _ticketRepository.UpdateAsync(updatedTicket);

            //var result = await _ticketRepository.GetByTicketNumberAsync(updatedTicket.TicketNumber);

            return;
        }
    }
}
