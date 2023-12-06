namespace Ticket.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int TicketNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int FromAirportId { get; set; }
        public Airport? FromAirport { get; set; }
        public int ToAirportId { get; set; }
        public Airport? ToAirport { get; set; }
    }
}
