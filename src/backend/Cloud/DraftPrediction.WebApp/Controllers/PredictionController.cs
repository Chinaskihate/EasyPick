using DraftPrediction.Contract.Entities;
using DraftPrediction.Contract.Entities.DataTransferObjects;
using DraftPrediction.Contract.Entities.DataTransferObjects.Drafts;
using DraftPrediction.Contract.Entities.Drafts;
using MessageBroker.Common;
using Microsoft.AspNetCore.Mvc;

namespace DraftPrediction.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionController : ControllerBase
{
    private readonly IMessageProducer<Draft> _producer;

    public PredictionController(IMessageProducer<Draft> producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Predict([FromBody] PredictRequest request)
    {
        _producer.SendAsync(new Draft()
        {
            Hero = new Hero() {HeroId = 1}
        });
        return Ok(Guid.NewGuid());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetPredictionResponse>> GetPrediction(Guid id)
    {
        return Ok(new GetPredictionResponse()
        {
            RadiantPicks = new List<PickDto>()
            {
                new PickDto()
                {
                    HeroId = 1,
                    Order = 0
                },
                new PickDto()
                {
                    HeroId = 2,
                    Order = 1
                },
                new PickDto()
                {
                    HeroId = 3,
                    Order = 2
                }
            },
            DirePicks = new List<PickDto>()
            {
                new PickDto()
                {
                    HeroId = 10,
                    Order = 0
                },
                new PickDto()
                {
                    HeroId = 11,
                    Order = 1
                },
                new PickDto()
                {
                    HeroId = 12,
                    Order = 2
                },
                new PickDto()
                {
                    HeroId = 13,
                    Order = 3
                }
            },
            Bans = Enumerable.Range(70,90).Select(x => new BanDto()
            {
                HeroId = x
            }).ToList()
        });
    }
}