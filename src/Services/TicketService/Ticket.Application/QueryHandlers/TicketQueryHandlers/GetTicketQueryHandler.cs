using Ticket.Persistence.Repositories.Interfaces;

namespace Ticket.Application.QueryHandlers.TicketQueryHandlers
{
    public class GetTicketQueryHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IAirportRepository _airportRepository;

        public GetTicketQueryHandler(
            ITicketRepository ticketRepository,
            IAirportRepository airportRepository)
        {
            _ticketRepository = ticketRepository;
            _airportRepository = airportRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Ticket>> Handle(int TicketNumber)
        {
            var result = new List<Domain.Entities.Ticket>();

            var ticket = await _ticketRepository.GetByTicketNumberAsync(TicketNumber);

            var departureAirport = await _airportRepository.GetByNameAsync(ticket.FromAirport.Name);

            result.Add(await _ticketRepository.GetByTicketNumberAsync(TicketNumber));

            result.AddRange(_ticketRepository.GetByOrigin(departureAirport.Name));

            return result;
        }
    }
}
