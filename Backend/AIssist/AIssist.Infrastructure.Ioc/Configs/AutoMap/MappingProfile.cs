using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Profile;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProfileRequest, Profiles>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.Profile, o => o.MapFrom(p => p.Profile))
            .ForMember(p => p.Updated_At, o => o.MapFrom(p => DateTime.Now));
        }
    }
}

