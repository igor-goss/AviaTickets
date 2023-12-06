using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.CommandHandlers.AirportCommandHandlers;
using Ticket.Application.DTO;
using Ticket.Application.QueryHandlers.AirportQueryHandlers;

namespace TicketServiceAPI.Controllers.Commands
{
    [Route("api/airport")]
    [ApiController]
    public class AirportCommandsController : ControllerBase
    {
        private readonly CreateAirportCommandHandler _createAirportCommandHandler;
        private readonly DeleteAirportCommandHandler _deleteAirportCommandHandler;
        private readonly UpdateAirportCommandHandler _updateAirportCommandHandler;

        public AirportCommandsController(
            CreateAirportCommandHandler createAirportCommandHandler,
            DeleteAirportCommandHandler deleteAirportCommandHandler,
            UpdateAirportCommandHandler updateAirportCommandHandler)
        {
            _createAirportCommandHandler = createAirportCommandHandler;
            _deleteAirportCommandHandler = deleteAirportCommandHandler;
            _updateAirportCommandHandler = updateAirportCommandHandler;
        }

        [HttpPost]
        public async Task CreateAirport(CreateAirportDTO createAirportDTO)
        {
            await _createAirportCommandHandler.Handle(createAirportDTO);
        }

        [HttpPatch]
        public async Task UpdateAirport(UpdateAirportDTO updateAirportDTO)
        {
            await _updateAirportCommandHandler.Handle(updateAirportDTO);
        }

        [HttpDelete]
        public async Task DeleteAirport(DeleteAirportDTO deleteAirportDTO)
        {
            await _deleteAirportCommandHandler.Handle(deleteAirportDTO);
        }
    }
}
