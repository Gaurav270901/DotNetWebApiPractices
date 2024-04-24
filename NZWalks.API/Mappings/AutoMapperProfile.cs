using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap().ReverseMap();
            CreateMap<Difficulty , DifficultyDTO>().ReverseMap();
            CreateMap<UpdatedWalkRequestDto, Walk>().ReverseMap();


        }
    }
}
