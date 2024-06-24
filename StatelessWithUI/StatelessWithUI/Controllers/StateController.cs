using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.PlaneState.Queries;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Controllers;

[Controller]
[Route("api/[controller]")]
public class StateController: ControllerBase
{
    private readonly IPlaneStateRepository _planeStateRepository;
    private readonly IStateService _stateService;
    private readonly IMediator _mediator;

    public StateController(IPlaneStateRepository planeStateRepository, IStateService stateService, IMediator mediator)
    {
        _planeStateRepository = planeStateRepository;
        _stateService = stateService;
        _mediator = mediator;
    }

    [HttpGet("states")]
    public async Task<ActionResult<GetAllPlaneStatesQueryResponseDto>> GetAllStates(PlaneStateMachine.PlaneState planeState)
    {
        var result = await _mediator.Send(new GetAllPlaneStatesQuery(planeState));
        
        if (result == null) return NotFound($"Plane State Not Found for planeState : {planeState}");

        return Ok(result);
    }

    [HttpGet("state")]
    public async Task<IActionResult> Get(string id, PlaneStateMachine.PlaneState planeState)
    {
        var result = await _mediator.Send(new GetPlaneStateQuery(id, planeState));
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id}");

        return Ok(result);
    }
    
}