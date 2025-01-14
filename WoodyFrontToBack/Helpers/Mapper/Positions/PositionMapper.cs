using AutoMapper;
using WoodyFrontToBack.DTOs.Positions;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.Helpers.Mapper.Positions;

public class PositionMapper : Profile
{
    public PositionMapper()
    {
        CreateMap<CreatePositionDto, Position>().ReverseMap();
        CreateMap<UpdatePositionDto, Position>().ReverseMap();
    }
}
