using AutoMapper;
using MatchParser.Contract.Models;
using MatchParser.Contract.Models.DataTransferObjects;

namespace MatchParser.Contract.Mappings;

public class MatchResultMappingProfile : Profile
{
    public MatchResultMappingProfile()
    {
        CreateMap<PickBanDto, PickBan>()
            .ForMember(dest => dest.IsPick,
                o => o.MapFrom(src => src.IsPick))
            .ForMember(dest => dest.HeroId,
                o => o.MapFrom(src => src.HeroId))
            .ForMember(dest => dest.Team,
                o => o.MapFrom(src => src.Team));
        CreateMap<MatchResultDto, MatchResult>()
            .ForMember(dest => dest.Id,
                o => o.MapFrom(src => src.Id))
            .ForMember(dest => dest.RadiantWin,
                o => o.MapFrom(src => src.RadiantWin))
            .ForMember(dest => dest.Duration,
                o => o.MapFrom(src => src.Duration))
            .ForMember(dest => dest.GameMode,
                o => o.MapFrom(src => src.GameMode))
            .ForMember(dest => dest.LobbyType,
                o => o.MapFrom(src => src.LobbyType))
            .ForMember(dest => dest.DireScore,
                o => o.MapFrom(src => src.DireScore))
            .ForMember(dest => dest.RadiantScore,
                o => o.MapFrom(src => src.RadiantScore))
            .ForMember(dest => dest.HumanPlayersCount,
                o => o.MapFrom(src => src.HumanPlayersCount))
            .ForMember(dest => dest.PickBans,
                o => o.MapFrom(src => src.PickBans));
    }
}