using MediatR;
using Ticket.Application.Queries.TicketQueries;
using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.TicketQueryHandlers
{
    public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, IEnumerable<IEnumerable<Domain.Entities.Ticket>>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetRouteQueryHandler(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<IEnumerable<Domain.Entities.Ticket>>> Handle(GetRouteQuery query, CancellationToken cancellationToken)
        {
            var tickets = _ticketRepository.FindRoutesBetweenCities(query.OriginCity, query.DestinationCity);

            return tickets;
        }
    }
}
