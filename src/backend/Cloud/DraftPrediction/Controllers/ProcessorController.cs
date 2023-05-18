using Common.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DraftPrediction.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessorController : ControllerBase
{
    private readonly ITaskManager<IDistributionTask> _taskManager;

    public ProcessorController(ITaskManager<IDistributionTask> taskManager)
    {
        _taskManager = taskManager;
    }

    [HttpPost("start")]
    public async Task Start()
    {
        _taskManager.StartAsync();
    }

    [HttpPost("stop")]
    public async Task Stop()
    {
        _taskManager.StopAsync();
    }
}