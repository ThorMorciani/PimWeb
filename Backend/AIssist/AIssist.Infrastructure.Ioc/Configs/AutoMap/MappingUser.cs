using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<UserRequest, Users>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.Name, o => o.MapFrom(p => p.Name))
            .ForMember(p => p.Username, o => o.MapFrom(p => p.Username))
            .ForMember(p => p.Password, o => o.MapFrom(p => p.Password))
            .ForMember(p => p.Email, o => o.MapFrom(p => p.Email))
            .ForMember(p => p.Profile_Id, o => o.MapFrom(p => p.profileId))
            .ForMember(p => p.Updated_At, o => o.MapFrom(p => DateTime.Now))
            .ForMember(p => p.Active, o => o.MapFrom(p => true));
        }
    }
}

