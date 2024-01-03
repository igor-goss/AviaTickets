using AutoMapper;
using Ticket.Application.Commands.AirportCommands;
using Ticket.Application.Commands.TicketCommands;
using Ticket.Application.DTO;
using Ticket.Domain.Entities;

namespace Ticket.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAirportCommand, Airport>();

            CreateMap<CreateTicketCommand, Domain.Entities.Ticket>();

            /*CreateMap<UpdateTicketCommand, Domain.Entities.Ticket>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.TicketNumber))
                .ForMember(dest => dest.DepartureDateTime, opt => opt.MapFrom(src => src.DepartureDateTime))
                .ForMember(dest => dest.ArrivalDateTime, opt => opt.MapFrom(src => src.ArrivalDateTime))
                .ForMember(dest => dest.FromAirportId, opt => opt.MapFrom(src => src.FromAirportId))
                .ForMember(dest => dest.FromAirport, opt => opt.MapFrom(src => src.FromAirport))
                .ForMember(dest => dest.ToAirportId, opt => opt.MapFrom(src => src.ToAirportId))
                .ForMember(dest => dest.ToAirport, opt => opt.MapFrom(src => src.ToAirport));*/

            CreateMap<CreateAirportDTO, Airport>();

            CreateMap<UpdateAirportDTO, Airport>();

            CreateMap<CreateTicketDTO, Domain.Entities.Ticket>();

            CreateMap<UpdateTicketDTO, Domain.Entities.Ticket>();

            CreateMap<DeleteAirportDTO, DeleteAirportCommand>();

            CreateMap<DeleteTicketDTO, DeleteTicketCommand>();
        }
    }
}
