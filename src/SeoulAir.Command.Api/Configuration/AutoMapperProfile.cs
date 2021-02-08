using System;
using AutoMapper;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Repositories.Entities;

namespace SeoulAir.Command.Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;

            #region Dtos - Entities

            CreateMap<BaseDtoWithId, BaseEntityWithId>().ReverseMap();
            CreateMap<CommandDto, Repositories.Entities.Command>()
                .ForMember(entity => entity.Id,
                    opt => opt.MapFrom(src => new Guid(src.Id)))
                .ReverseMap()
                .ForPath(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            #endregion
        }
    }
}
