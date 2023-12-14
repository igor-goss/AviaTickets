using MediatR;

namespace Ticket.Application.Queries.TicketQueries
{
    public class GetTicketQuery : IRequest<Domain.Entities.Ticket>
    {
        public int TicketNumber { get; set; }
    }
}
