namespace Ticket.Persistence.Repositories.Interfaces
{
    public interface ITicketRepository : IRepository<Domain.Entities.Ticket>
    {
        public IEnumerable<Domain.Entities.Ticket> GetByOrigin(string airportName);
        public IEnumerable<Domain.Entities.Ticket> GetByDestianation(string airportName);
        public Task<Domain.Entities.Ticket> GetByTicketNumberAsync(int ticket);
        public IEnumerable<IEnumerable<Domain.Entities.Ticket>> FindRoutesBetweenCities(string originCity, string destinationCity);

    }
}
