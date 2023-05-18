using DraftPrediction.Contract.Models.DataTransferObjects;
using MessageBroker.Common;
using Microsoft.AspNetCore.Mvc;
using ModelPredictionRedirector.Services;

namespace ModelPredictionRedirector.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueueRedirectController : ControllerBase
{
    private readonly IMessageProducer<RecommendationsDto> _producer;
    private readonly RequestsStorage _storage;

    public QueueRedirectController(
        IMessageProducer<RecommendationsDto> producer,
        RequestsStorage storage)
    {
        _producer = producer;
        _storage = storage;
    }

    [HttpPost(Name = "PushPrediction")]
    public async Task<ActionResult> PushToQueue([FromBody] RecommendationsDto dto)
    {
        await _producer.SendAsync(dto);
        return Ok();
    }

    [HttpGet(Name = "GetPredictions")]
    public async Task<ActionResult<List<PredictDraftDto>>> GetFromQueue()
    {
        var requests = _storage.PopRequests();
        return Ok(requests.ToList());
        //return Ok(new List<PredictDraftDto>()
        //{
        //    new PredictDraftDto()
        //    {
        //        RequestId = Guid.NewGuid(),
        //        Bans = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 1,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 2,
        //            }
        //        },
        //        DirePicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 20,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 21,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 22,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 23,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 24,
        //            }
        //        },
        //        RadiantPicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 30,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 31,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 32,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 33,
        //            },
        //        },
        //        RecommendedPosition = Position.Carry,
        //    },
        //    new PredictDraftDto()
        //    {
        //        RequestId = Guid.NewGuid(),
        //        Bans = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 1,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 2,
        //            }
        //        },
        //        DirePicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 20,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 21,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 22,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 23,
        //            }
        //        },
        //        RadiantPicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 30,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 31,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 32,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 33,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 34,
        //            }
        //        },
        //        RecommendedPosition = Position.OffLaner,
        //    },
        //    new PredictDraftDto()
        //    {
        //        RequestId = Guid.NewGuid(),
        //        Bans = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 1,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 2,
        //            }
        //        },
        //        DirePicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 20,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 21,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 22,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 23,
        //            }
        //        },
        //        RadiantPicks = new List<DraftDto>()
        //        {
        //            new DraftDto()
        //            {
        //                HeroId = 30,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 31,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 32,
        //            },
        //            new DraftDto()
        //            {
        //                HeroId = 33,
        //            }
        //        },
        //        RecommendedPosition = Position.SemiSupport,
        //    },
        //});
    }
}