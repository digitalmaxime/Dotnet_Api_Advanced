using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.PlaneState.Commands;
using StatelessWithUI.Application.Features.PlaneState.Queries;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Controllers;

[Controller]
[Route("api/[controller]")]
public class PlaneStateController: ControllerBase
{
    private readonly IPlaneStateRepository _planeStateRepository;
    private readonly IStateService _stateService;
    private readonly IMediator _mediator;

    public PlaneStateController(IPlaneStateRepository planeStateRepository, IStateService stateService, IMediator mediator)
    {
        _planeStateRepository = planeStateRepository;
        _stateService = stateService;
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<ActionResult<GetAllPlaneStatesQueryResponseDto>> GetAllStates(PlaneStateMachine.PlaneState planeState)
    {
        var result = await _mediator.Send(new GetAllPlaneStatesQuery(planeState));
        
        if (result == null) return NotFound($"Plane State Not Found for planeState : {planeState}");

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id, PlaneStateMachine.PlaneState planeState)
    {
        var result = await _mediator.Send(new GetPlaneStateQuery(id, planeState));
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id}");

        return Ok(result);
    }
    
    [HttpGet("initial/{id}")]
    public async Task<IActionResult> GetInitialState(string id)
    {
        var result = await _mediator.Send(new GetPlaneStateQuery(id, PlaneStateMachine.PlaneState.InitialState));
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id} at Initial State");

        return Ok(result);
    }
    
    [HttpGet("design/{id}")]
    public async Task<IActionResult> GetDesignState(string id)
    {
        var result = await _mediator.Send(new GetPlaneStateQuery(id, PlaneStateMachine.PlaneState.DesignState));
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id} at Design State");

        return Ok(result);
    }
    
    [HttpGet("build/{id}")]
    public async Task<IActionResult> GetBuildState(string id)
    {
        var result = await _mediator.Send(new GetPlaneBuildStateQuery(id));
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id} at Design State");

        return Ok(result);
    }
    
    [HttpPost("plane-state")]
    public async Task<IActionResult> CreatePlaneState(CreatePlaneBuildStateCommand createPlaneBuildStateCommand)
    {
        var result = await _mediator.Send(createPlaneBuildStateCommand);
        
        if (result == null) return NotFound($"Enable to create state for planeId : {createPlaneBuildStateCommand} at Build State");

        return Ok(result);
    }
    
}