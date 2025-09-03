using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.RootCause;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingRootCause : Profile
    {
        public MappingRootCause()
        {
            CreateMap<RootCauseRequest, RootCauses>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.Root_Cause, o => o.MapFrom(p => p.RootCause))
            .ForMember(p => p.Created_By, o => o.MapFrom(p => p.CreatedBy))
            .ForMember(p => p.Criticality_Id, o => o.MapFrom(p => p.CriticalityId))
            .ForMember(p => p.Updated_At, o => o.MapFrom(p => DateTime.Now));
        }
    }
}

