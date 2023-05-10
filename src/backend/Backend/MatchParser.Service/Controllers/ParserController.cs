using Common.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MatchParser.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParserController : ControllerBase
{
    private readonly ITaskManager<IDistributionTask> _taskManager;

    public ParserController(ITaskManager<IDistributionTask> taskManager)
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