using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.Plane.Commands;
using StatelessWithUI.Application.Features.Plane.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaneController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlaneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IEnumerable<PlaneEntity>>? Get()
    {
        return await _mediator.Send(new GetAllPlaneQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPlaneQueryResponseDto>> Get(string id, [FromQuery] bool includes)
    {
        var plane = await _mediator.Send(new GetPlaneQuery(id, includes));
        if (plane == null)
        {
            return NotFound("Plane not found");
        }

        return Ok(plane);
    }

    [HttpGet("getpermittedtriggers/{id}")]
    public async Task<ActionResult<IEnumerable<string>>> GetPermittedTriggers(string id)
    {
        var result = await _mediator.Send(new GetPlaneGetPermittedTriggersQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        var createdPlane = await _mediator.Send(new CreatePlaneCommand());
        if (createdPlane == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return CreatedAtAction(nameof(Get), new { id = createdPlane.Id }, createdPlane);
    }

    [HttpPost("action/{id}")]
    public async Task<IActionResult> TakeAction(string id, [FromQuery] string action)
    {
        var result = await _mediator.Send(new TakePlaneActionCommand(id, action));
        return result ? Ok() : BadRequest();
    }
}