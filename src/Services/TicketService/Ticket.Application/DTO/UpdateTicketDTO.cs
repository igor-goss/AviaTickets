namespace Ticket.Application.DTO
{
    public class UpdateTicketDTO
    {
        public decimal Price { get; set; }
        public int TicketNumber { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int FromAirportId { get; set; }
        public int ToAirportId { get; set; }
    }
}
