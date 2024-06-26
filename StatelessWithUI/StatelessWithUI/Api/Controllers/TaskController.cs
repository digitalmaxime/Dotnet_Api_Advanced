using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.PlaneState.Queries;
using StatelessWithUI.Application.Features.Tasks.Commands;
using StatelessWithUI.Application.Features.Tasks.Queries;

namespace StatelessWithUI.Controllers;

[Controller]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTasks()
    {
        var result = await _mediator.Send(new GetAllBuildPlaneTaskQuery());

        if (result == null) return NotFound($"No tasks found");

        return Ok(result);
    }

    [HttpGet("build-task/{id}")]
    public async Task<IActionResult> GetBuildTask(string id)
    {
        var result = await _mediator.Send(new GetPlaneBuildTaskQuery(id));

        if (result == null) return NotFound($"Plane task Not Found for taskid : {id}");

        return Ok(result);
    }

    [HttpPost("build-task/complete/{id}")]
    public async Task<IActionResult> CompleteBuildTask(string id)
    {
        var result = await _mediator.Send(new CompletePlaneBuildTaskCommand(id));

        if (result == null) return NotFound($"Plane task Not Found for taskid : {id}");

        return Ok(result);
    }

    [HttpPost("build-task")]
    public async Task<IActionResult> CreateBuildTask(CreatePlaneBuildTaskCommandDto request)
    {
        var result = await _mediator.Send(request);

        if (result == null) return NotFound($"Plane build state not found with buildStateId : {request.planeStateId}");

        return Ok(result);
    }
}