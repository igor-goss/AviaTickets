using AutoMapper;
using MediatR;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.CommandHandlers.TicketCommandHandlers
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public DeleteTicketCommandHandler(
            ITicketRepository ticketRepository,
            IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
        {
            await _ticketRepository.DeleteAsync(command.Id);

            return;
        }
    }
}
