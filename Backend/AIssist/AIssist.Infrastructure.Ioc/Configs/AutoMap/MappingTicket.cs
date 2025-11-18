using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Ticket;
using AIssist.Domain.Http.Response.RootCause;
using AIssist.Domain.Http.Response.Tickets;
using AIssist.Domain.Http.Response.Users;
using AIssist.Shared.Helpers;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
	public class MappingTicket : Profile
	{
		public MappingTicket()
		{
			CreateMap<TicketPostRequest, Tickets>()
				.ForMember(p => p.Description, o => o.MapFrom(p => p.Description))
				.ForMember(p => p.Solution, o => o.MapFrom(p => p.Solution))
				.ForMember(p => p.ReporterId, o => o.MapFrom(p => p.ReporterId))
				.ForMember(p => p.RootCauseId, o => o.MapFrom(p => p.RootCauseId))
                .ForMember(p => p.Status, o => o.MapFrom(p => Domain.Enums.TicketStatus.Aberto))
                .ForMember(p => p.CreatedAt, o => o.MapFrom(p => DateTime.Now))
                .ForMember(p => p.UpdatedAt, o => o.MapFrom(p => DateTime.Now))
                .ForMember(p => p.TicketNumber, o => o.MapFrom(p => string.Concat("INC", Guid.NewGuid().ToString("N").Substring(0, 8))));

            CreateMap<Tickets, TicketResponse>()
                .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.TicketNumber))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => (long)src.Status))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDescription()))
                .ForMember(dest => dest.RootCause, opt => opt.MapFrom(src => src.RootCause))
                .ForMember(dest => dest.Reporter, opt => opt.MapFrom(src => src.Reporter))
                .ForMember(dest => dest.Assignee, opt => opt.MapFrom(src => src.Assignee))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime));

            CreateMap<RootCause, RootCauseResponse>();

            CreateMap<Users, UserResponse>();

            CreateMap<TicketPutRequest, Tickets>()
                .ForMember(p => p.TicketNumber, o => o.MapFrom(p => p.TicketNumber))
                .ForMember(p => p.Description, o => o.MapFrom(p => p.Description))
                .ForMember(p => p.Solution, o => o.MapFrom(p => p.Solution));
        }
	}
}

