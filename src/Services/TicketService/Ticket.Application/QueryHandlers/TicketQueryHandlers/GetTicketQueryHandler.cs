using MediatR;
using Ticket.Application.Queries.TicketQueries;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.TicketQueryHandlers
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Domain.Entities.Ticket>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketQueryHandler(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Domain.Entities.Ticket> Handle(GetTicketQuery query, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByTicketNumberAsync(query.TicketNumber);

            return ticket;
        }
    }
}
