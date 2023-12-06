using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.TicketQueryHandlers
{
    public class GetRouteQueryHandler
    {
        private readonly ITicketRepository _ticketRepository;

        public GetRouteQueryHandler(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IEnumerable<IEnumerable<Domain.Entities.Ticket>> Handle(string originCity, string destinationCity)
        {
            var tickets = _ticketRepository.FindRoutesBetweenCities(originCity, destinationCity);

            return tickets;
        }
    }
}
