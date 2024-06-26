using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class Test: ControllerBase
{
    private readonly IPlaneService _planeService;

    public Test(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    [HttpPost("create-default-plane")]
    public async Task<ActionResult<PlaneDto>> CreateDefaultPlane()
    {
        var planeEntity = await _planeService.CreatePlaneAtInitialStateAsync();
        var responseDto = new PlaneDto()
        {
            PlaneId = planeEntity.Id,
            CurrentStateName = planeEntity.GetCurrentStateEnumName(),
            PlaneStateDtos = planeEntity.PlaneStates.Select(x => new PlaneStateDto()
            {
                StateId = x.Id,
                StateName = x.StateName,
                TaskDtos = x.StateTask.Select(y => new TaskDto()
                {
                    TaskId = y.Id,
                    TaskName = y.TaskName,
                    IsComplete = y.IsComplete
                })
            })
        };

        return responseDto;
    }
    
    [HttpPost("create-plane-at-build-state")]
    public async Task<ActionResult<PlaneDto>> CreatePlaneAtBuildState()
    {
        var planeEntity = await _planeService.CreatePlaneAtBuildStateAsync();
        var responseDto = new PlaneDto()
        {
            PlaneId = planeEntity.Id,
            CurrentStateName = planeEntity.GetCurrentStateEnumName(),
            PlaneStateDtos = planeEntity.PlaneStates.Select(x => new PlaneStateDto()
            {
                StateId = x.Id,
                StateName = x.StateName,
                TaskDtos = x.StateTask.Select(y => new TaskDto()
                {
                    TaskId = y.Id,
                    TaskName = y.TaskName,
                    IsComplete = y.IsComplete
                })
            })
        };

        return responseDto;
    }
}

public class PlaneDto
{
    public string PlaneId { get; set; }
    public IEnumerable<PlaneStateDto> PlaneStateDtos { get; set; }
    public string CurrentStateName { get; set; }
}

public class PlaneStateDto
{
    public string StateId { get; set; }
    public string StateName { get; set; }
    public IEnumerable<TaskDto> TaskDtos { get; set; }
}

public class TaskDto
{
    public string TaskId { get; set; }
    public string TaskName { get; set; }
    public bool IsComplete { get; set; }
} 