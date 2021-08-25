using System.Linq;
using API.Controllers;
using API.DTO;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,memberDto>()
            .ForMember(mem => mem.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(mem => mem.age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDto>();
            CreateMap<UpdateDto, AppUser>();

        }
    }
}