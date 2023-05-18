using AutoMapper;
using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;
using DraftPrediction.Contract.Models.Drafts;

namespace DraftPrediction.Contract.Mappings;

public class PredictionMappingProfile : Profile
{
    public PredictionMappingProfile()
    {
        CreateMap<PredictRequest, PredictInfo>()
            .ForMember(i => i.DirePicks,
                o => o.MapFrom(r => r.DirePicks))
            .ForMember(i => i.Bans,
                o => o.MapFrom(r => r.Bans))
            .ForMember(i => i.RadiantPicks,
                o => o.MapFrom(r => r.RadiantPicks))
            .ForMember(i => i.RecommendedPosition,
                o => o.MapFrom(r => r.RecommendedPosition));

        CreateMap<Prediction, GetPredictionResponse>()
            .ForMember(r => r.DirePicks,
                o => o.MapFrom(p => p.DirePicks))
            .ForMember(r => r.RadiantPicks,
                o => o.MapFrom(p => p.RadiantPicks))
            .ForMember(r => r.Bans,
                o => o.MapFrom(p => p.Bans))
            .ForMember(r => r.RecommendedPosition,
                o => o.MapFrom(p => p.RecommendedPosition));

        CreateMap<Prediction, PredictDraftDto>()
            .ForMember(dto => dto.RequestId,
                o => o.MapFrom(p => p.Id))
            .ForMember(dto => dto.RecommendedPosition,
                o => o.MapFrom(p => p.RecommendedPosition))
            .ForMember(dto => dto.Bans,
                o => o.MapFrom(p => p.Bans))
            .ForMember(dto => dto.RadiantPicks,
                o => o.MapFrom(p => p.RadiantPicks))
            .ForMember(dto => dto.DirePicks,
                o => o.MapFrom(p => p.DirePicks))
            .ReverseMap()
            .ForMember(p => p.Id,
                o => o.MapFrom(dto => dto.RequestId))
            .ForMember(p => p.RecommendedPosition,
                o => o.MapFrom(dto => dto.RecommendedPosition))
            .ForMember(p => p.Bans,
                o => o.MapFrom(dto => dto.Bans))
            .ForMember(p => p.RadiantPicks,
                o => o.MapFrom(dto => dto.RadiantPicks))
            .ForMember(p => p.DirePicks,
                o => o.MapFrom(dto => dto.DirePicks));

        CreateMap<DraftDto, Draft>()
            .ForMember(p => p.Hero,
                o => o.MapFrom(dto => new Hero()
                {
                    HeroId = dto.HeroId ?? 100
                }))
            .ReverseMap()
            .ForMember(dto => dto.HeroId,
                o => o.MapFrom(p => p.Hero.HeroId));

        CreateMap<RecommendedDraftDto, RecommendedDraft>()
            .ForMember(p => p.Hero,
                o => o.MapFrom(dto => new Hero()
                {
                    HeroId = dto.HeroId
                }))
            .ReverseMap()
            .ForMember(dto => dto.HeroId,
                o => o.MapFrom(p => p.Hero.HeroId));
    }
}