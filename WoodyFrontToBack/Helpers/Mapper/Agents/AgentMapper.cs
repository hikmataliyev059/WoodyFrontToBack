using AutoMapper;
using WoodyFrontToBack.DTOs.Agents;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.Helpers.Mapper.Agents;

public class AgentMapper : Profile
{
    public AgentMapper()
    {
        CreateMap<CreateAgentDto, Agent>().ReverseMap();
        CreateMap<UpdateAgentDto, Agent>().ReverseMap();
    }
}
