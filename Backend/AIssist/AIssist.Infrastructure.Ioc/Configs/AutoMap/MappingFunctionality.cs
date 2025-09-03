using AIssist.Domain.Entities;
using AIssist.Domain.Http.Request.Functionality;
using AutoMapper;

namespace AIssist.Infrastructure.Ioc.Configs.AutoMap
{
    public class MappingFunctionality : Profile
    {
        public MappingFunctionality()
        {
            CreateMap<FunctionalityRequest, Functionalities>()
            .ForMember(p => p.Id, o => o.MapFrom(p => p.Id))
            .ForMember(p => p.Description, o => o.MapFrom(p => p.Functionality))
            .ForMember(p => p.Created_By, o => o.MapFrom(p => p.CreatedBy))
            .ForMember(p => p.Updated_At, o => o.MapFrom(p => DateTime.Now));
        }
    }
}

