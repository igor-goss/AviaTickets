using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.DTO;
using Ticket.Application.Queries.AirportQueries;
using Ticket.Domain.Entities;

namespace TicketServiceAPI.Controllers.Commands
{
    [Route("api/airport")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Airport>> GetAirport(string? Name, string? Abbreviation, string? City, string? Country)
        {
            var result = await _mediator.Send(new GetAirportsQuery() {
                Name = Name, Abbreviation = Abbreviation, City = City, Country = Country
            });

            return result;
        }

        [HttpPost]
        public async Task CreateAirport([FromBody]CreateAirportDTO createAirportDTO)
        {
            await _mediator.Send(new CreateAirportCommand() { CreateAirportDTO = createAirportDTO});
        }

        [HttpPatch]
        public async Task UpdateAirport(UpdateAirportDTO updateAirportDTO)
        {
            await _mediator.Send(new UpdateAirportCommand() {  UpdateAirportDTO = updateAirportDTO });
        }

        [HttpDelete]
        public async Task DeleteAirport(DeleteAirportDTO deleteAirportDTO)
        {
            await _mediator.Send(new DeleteAirportCommand() { Id = deleteAirportDTO.Id});
        }
    }
}
