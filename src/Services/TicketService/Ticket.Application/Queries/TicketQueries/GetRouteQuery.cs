using MediatR;

namespace Ticket.Application.Queries.TicketQueries
{
    public class GetRouteQuery : IRequest<IEnumerable<IEnumerable<Domain.Entities.Ticket>>>
    {
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
    }
}
