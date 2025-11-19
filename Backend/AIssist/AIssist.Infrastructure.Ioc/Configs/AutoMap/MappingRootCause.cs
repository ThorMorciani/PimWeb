using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;
using AIssist.Domain.Http.Response.RootCause;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingRootCause : Profile
    {
        public MappingRootCause()
        {
            CreateMap<RootCausePutRequest, RootCause>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.RootCauseName, o => o.MapFrom(p => p.RootCause))
            .ForMember(p => p.CreatedBy, o => o.MapFrom(p => p.CreatedBy))
            .ForMember(p => p.Criticality, o => o.MapFrom(p => p.CriticalityId))
            .ForMember(p => p.UpdatedAt, o => o.MapFrom(p => DateTime.Now));

            CreateMap<RootCausePostRequest, RootCause>()
            .ForMember(p => p.RootCauseName, o => o.MapFrom(p => p.RootCause))
            .ForMember(p => p.Criticality, o => o.MapFrom(p => p.CriticalityId))
            .ForMember(p => p.UpdatedBy, o => o.MapFrom(p => p.CreatedBy))
            .ForMember(p => p.CreatedBy, o => o.MapFrom(p => p.CreatedBy))
            .ForMember(p => p.UpdatedAt, o => o.MapFrom(p => DateTime.Now))
             .ForMember(p => p.CreatedAt, o => o.MapFrom(p => DateTime.Now))
            .ForMember(p => p.Active, o => o.MapFrom(p => true));

            CreateMap<RootCause, RootCauseResponse>()
                .ForMember(dest => dest.UpdatedAt,opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")));
        }
    }
}

