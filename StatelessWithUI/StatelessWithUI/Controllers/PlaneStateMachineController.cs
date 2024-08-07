using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.PlaneStateMachine.Commands;
using StatelessWithUI.Application.Features.PlaneStateMachine.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaneStateMachineController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlaneStateMachineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("plane")]
    public async Task<IEnumerable<PlaneEntity>>? Get()
    {
        return await _mediator.Send(new GetAllPlaneQuery());
    }

    [HttpGet("plane/{id}")]
    public async Task<PlaneEntity?> Get(string id)
    {
        var plane = await _mediator.Send(new GetPlaneQuery(id));
        return plane;
    }

    [HttpGet("plane/getpermittedtriggers/{id}")]
    public async Task<IEnumerable<string>> GetPermittedTriggers(string id)
    {
        return await _mediator.Send(new GetPlaneGetPermittedTriggersQuery(id));
    }

    [HttpPost("plane")]
    public async Task<IActionResult> Create(PlaneEntity plane)
    {
        var createdPlane = await _mediator.Send(new CreatePlaneCommand(plane.Id));
        if (createdPlane == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        return CreatedAtAction(nameof(Get), new { id = createdPlane.Id }, createdPlane);
    }

    [HttpPost("plane/action/{id}")]
    public async Task<IActionResult> TakeAction(string id, [FromQuery] string action)
    {
        await _mediator.Send(new TakePlaneActionCommand(id, action));
        return Ok();    
    }}