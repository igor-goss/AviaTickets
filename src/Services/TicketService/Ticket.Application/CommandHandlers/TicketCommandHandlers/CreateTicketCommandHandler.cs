using AutoMapper;
using MediatR;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.TicketCommandHandlers
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
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

        public async Task Handle(CreateTicketCommand command, CancellationToken cancellationToken)
        {
            var existingTicket = await _ticketRepository.GetByTicketNumberAsync(command.CreateTicketDTO.TicketNumber);

            if (existingTicket != null)
            {
                throw new InvalidOperationException($"A ticket with the number {command.CreateTicketDTO.TicketNumber} already exists.");
            }

            var ticket = _mapper.Map<Domain.Entities.Ticket>(command.CreateTicketDTO);

            await _ticketRepository.AddAsync(ticket);

            return;
        }
    }
}
