using AutoMapper;
using DraftPrediction.Contract.Application;
using DraftPrediction.Contract.Models;
using DraftPrediction.Contract.Models.DataTransferObjects;
using DraftPrediction.Contract.Models.DataTransferObjects.Drafts;
using Microsoft.AspNetCore.Mvc;

namespace DraftPrediction.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionController : ControllerBase
{
    private readonly IPredictionProvider _provider;
    private readonly IPredictionManager _manager;
    private readonly IMapper _mapper;

    public PredictionController(
        IPredictionProvider provider,
        IPredictionManager manager,
        IMapper mapper)
    {
        _provider = provider;
        _manager = manager;
        _mapper = mapper;
    }

    [HttpPost(Name = "Predict")]
    public async Task<ActionResult<Guid>> Predict([FromBody] PredictRequest request)
    {
        var info = _mapper.Map<PredictRequest, PredictInfo>(request);
        return Ok(await _manager.Predict(info, CancellationToken.None));
    }

    [HttpGet(Name = "Predict/{id:guid}")]
    public async Task<ActionResult<GetPredictionResponse>> Get(Guid id)
    {
        //var prediction = await _provider.GetPredictionAsync(id, CancellationToken.None);
        //var res = _mapper.Map<GetPredictionResponse>(prediction);
        return Ok(new GetPredictionResponse()
        {
            IsFinished = true,
            RecommendedRadiantPicks = new List<RecommendedDraftDto>()
            {
                new RecommendedDraftDto()
                {
                    HeroId = 70,
                    WinProbability = 0.70
                },
                new RecommendedDraftDto()
                {
                    HeroId = 71,
                    WinProbability = 0.71
                },
                new RecommendedDraftDto()
                {
                    HeroId = 72,
                    WinProbability = 0.72
                },
                new RecommendedDraftDto()
                {
                    HeroId = 73,
                    WinProbability = 0.73
                },
                new RecommendedDraftDto()
                {
                    HeroId = 74,
                    WinProbability = 0.74
                },
                new RecommendedDraftDto()
                {
                    HeroId = 75,
                    WinProbability = 0.75
                },
                new RecommendedDraftDto()
                {
                    HeroId = 76,
                    WinProbability = 0.76
                },
                new RecommendedDraftDto()
                {
                    HeroId = 77,
                    WinProbability = 0.77
                },
                new RecommendedDraftDto()
                {
                    HeroId = 78,
                    WinProbability = 0.78
                },
                new RecommendedDraftDto()
                {
                    HeroId = 79,
                    WinProbability = 0.79
                }
            },
            RecommendedDirePicks = new List<RecommendedDraftDto>()
            {
                new RecommendedDraftDto()
                {
                    HeroId = 50,
                    WinProbability = 0.50
                },
                new RecommendedDraftDto()
                {
                    HeroId = 51,
                    WinProbability = 0.51
                },
                new RecommendedDraftDto()
                {
                    HeroId = 52,
                    WinProbability = 0.52
                },
                new RecommendedDraftDto()
                {
                    HeroId = 53,
                    WinProbability = 0.53
                },
                new RecommendedDraftDto()
                {
                    HeroId = 54,
                    WinProbability = 0.54
                },
                new RecommendedDraftDto()
                {
                    HeroId = 55,
                    WinProbability = 0.55
                },
                new RecommendedDraftDto()
                {
                    HeroId = 56,
                    WinProbability = 0.56
                },
                new RecommendedDraftDto()
                {
                    HeroId = 57,
                    WinProbability = 0.57
                },
                new RecommendedDraftDto()
                {
                    HeroId = 58,
                    WinProbability = 0.58
                },
                new RecommendedDraftDto()
                {
                    HeroId = 59,
                    WinProbability = 0.59
                }
            },
            Bans = new List<DraftDto>()
            {
                new DraftDto()
                {
                    HeroId = 1,
                },
                new DraftDto()
                {
                    HeroId = 2,
                }
            },
            DirePicks = new List<DraftDto>()
            {
                new DraftDto()
                {
                    HeroId = 20,
                },
                new DraftDto()
                {
                    HeroId = 21,
                },
                new DraftDto()
                {
                    HeroId = 22,
                },
                new DraftDto()
                {
                    HeroId = 23,
                },
                new DraftDto()
                {
                    HeroId = 24,
                }
            },
            RadiantPicks = new List<DraftDto>()
            {
                new DraftDto()
                {
                    HeroId = 30,
                },
                new DraftDto()
                {
                    HeroId = 31,
                },
                new DraftDto()
                {
                    HeroId = 32,
                },
                new DraftDto()
                {
                    HeroId = 33,
                },
            },
            RecommendedPosition = Position.MidLaner.ToString()
        });
    }
}