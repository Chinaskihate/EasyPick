using AutoMapper;
using DraftPrediction.Contract.Application;
using DraftPrediction.Contract.Models.DataTransferObjects;
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
        var prediction = await _provider.GetPredictionAsync(id, CancellationToken.None);
        return Ok(_mapper.Map<GetPredictionResponse>(prediction));
    }
}