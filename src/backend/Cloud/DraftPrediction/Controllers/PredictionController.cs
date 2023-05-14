using DraftPrediction.Contract.Entities.DataTransferObjects;
using DraftPrediction.Contract.Entities.DataTransferObjects.Drafts;
using Microsoft.AspNetCore.Mvc;

namespace DraftPrediction.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionController : ControllerBase
{

    public PredictionController()
    {
    }

    [HttpPost(Name = "Predict")]
    public ActionResult<Guid> Predict([FromBody] PredictRequest request)
    {
        return Guid.NewGuid();
    }

    [HttpGet(Name = "Predict/{id:guid}")]
    public ActionResult Get(Guid id)
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
            Bans = Enumerable.Range(70, 90).Select(x => new BanDto()
            {
                HeroId = x
            }).ToList()
        });
    }
}