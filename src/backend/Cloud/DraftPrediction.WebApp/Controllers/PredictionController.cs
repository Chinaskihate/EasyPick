using DraftPrediction.Contract.Application.Prediction;
using Microsoft.AspNetCore.Mvc;

namespace DraftPrediction.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictionController : ControllerBase
{
    private readonly ILogger<PredictionController> _logger;
    public PredictionController(ILogger<PredictionController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "predict")]
    public ActionResult<PredictionResponse> Predict([FromBody] PredictionRequest request)
    {
        _logger.LogInformation("predict");
        return Ok(null);
    }
}