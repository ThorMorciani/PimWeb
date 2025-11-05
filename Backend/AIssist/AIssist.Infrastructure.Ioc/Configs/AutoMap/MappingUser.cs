using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.User;
using AIssist.Domain.Http.Response.Users;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<UserPutRequest, Users>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.Name, o => o.MapFrom(p => p.Name))
            .ForMember(p => p.Username, o => o.MapFrom(p => p.Username))
            .ForMember(p => p.Email, o => o.MapFrom(p => p.Email))
            .ForMember(p => p.ProfileId, o => o.MapFrom(p => p.ProfileId))
            .ForMember(p => p.UpdatedAt, o => o.MapFrom(p => DateTime.Now))
            .ForMember(p => p.Active, o => o.MapFrom(p => true))
            .ForMember(p => p.Password, o => o.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
            {
                    if (srcMember == null) return false;
                    if (srcMember is string s && string.IsNullOrWhiteSpace(s)) return false;
                    if (srcMember is long l && l == 0) return false;
                    return true;
            }));

            CreateMap<UserPostRequest, Users>()
            .ForMember(p => p.Name, o => o.MapFrom(p => p.Name))
            .ForMember(p => p.Username, o => o.MapFrom(p => p.Username))
            .ForMember(p => p.Password, o => o.MapFrom(p => p.Password))
            .ForMember(p => p.Email, o => o.MapFrom(p => p.Email))
            .ForMember(p => p.ProfileId, o => o.MapFrom(p => p.ProfileId))
            .ForMember(p => p.UpdatedAt, o => o.MapFrom(p => DateTime.Now))
            .ForMember(p => p.CreatedAt, o => o.MapFrom(p => DateTime.Now))
            .ForMember(p => p.Active, o => o.MapFrom(p => true));

            CreateMap<Users, UserResponse>()
            .ForMember(p => p.Name, o => o.MapFrom(p => p.Name))
            .ForMember(p => p.Username, o => o.MapFrom(p => p.Username))
            .ForMember(p => p.Email, o => o.MapFrom(p => p.Email))
            .ForMember(p => p.Profile, o => o.MapFrom(p => p.ProfileId.ToString()))
            .ForMember(p => p.Updated_At, o => o.MapFrom(p => p.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(p => p.Created_At, o => o.MapFrom(p => p.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(p => p.Active, o => o.MapFrom(p => p.Active));
        }
    }
}

