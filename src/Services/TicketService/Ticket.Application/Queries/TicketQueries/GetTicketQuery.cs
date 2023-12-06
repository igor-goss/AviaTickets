namespace Ticket.Application.Queries.TicketQueries
{
    public class GetTicketQuery
    {
        public int TicketNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string? OriginAirportName { get; set; }
    }
}
