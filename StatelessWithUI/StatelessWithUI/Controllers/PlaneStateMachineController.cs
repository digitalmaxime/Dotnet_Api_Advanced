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
    public async Task<IEnumerable<PlaneVehicleEntity>>? Get()
    {
        return await _mediator.Send(new GetAllPlaneQuery());
    }

    [HttpGet("plane/{id}")]
    public async Task<PlaneVehicleEntity?> Get(string id)
    {
        var plane = await _mediator.Send(new GetPlaneQuery(id));
        return plane;
    }

    [HttpGet("plane/getpermittedtriggers/{id}")]
    public async Task<ActionResult<IEnumerable<string>>> GetPermittedTriggers(string id)
    {
        var result =await _mediator.Send(new GetPlaneGetPermittedTriggersQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("plane")]
    public async Task<IActionResult> Create(PlaneVehicleEntity planeVehicle)
    {
        var createdPlane = await _mediator.Send(new CreatePlaneCommand(planeVehicle.Id));
        if (createdPlane == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        return CreatedAtAction(nameof(Get), new { id = createdPlane.Id }, createdPlane);
    }

    [HttpPost("plane/action/{id}")]
    public async Task<IActionResult> TakeAction(string id, [FromQuery] string action)
    {
        var result = await _mediator.Send(new TakePlaneActionCommand(id, action));
        return result ? Ok() : BadRequest();    
    }}