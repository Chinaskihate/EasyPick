using AutoMapper;
using MatchParser.Contract.Models;
using MatchParser.Contract.Models.DataTransferObjects;

namespace MatchParser.Contract.Mappings;

public class MatchStampMappingProfile : Profile
{
    public MatchStampMappingProfile()
    {
        CreateMap<MatchStampDto, MatchStamp>()
            .ForMember(dest => dest.Id,
                o => o.MapFrom(src => src.MatchId));
        CreateMap<MatchStamp, MatchStampDto>()
            .ForMember(dest => dest.MatchId,
                o => o.MapFrom(src => src.Id));
    }
}