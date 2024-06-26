using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;

namespace StatelessWithUI.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class StateMachineController: ControllerBase
{
    private readonly IPlaneStateMachineService _planeStateMachineService;
    public StateMachineController(IPlaneStateMachineService planeStateMachineService)
    {
        _planeStateMachineService = planeStateMachineService;
    }
    
    [HttpGet("GetPermittedTriggers/{vehicleId}")]
    public async Task<ActionResult<IEnumerable<string>>> GetPermittedTriggers(string vehicleId)
    {
        var result = await _planeStateMachineService.GetPermittedTriggers(vehicleId);
        return Ok(result);
    }
    
    [HttpPost("TakeAction/{vehicleId}")]
    public async Task<IActionResult> TakeAction(string vehicleId, [FromQuery] PlaneAction action)
    {
        var result = await _planeStateMachineService.TakeActionAsync(vehicleId, action);
        return Ok(result);
    }
    
    [HttpGet("GetCurrentState/{vehicleId}")]
    public IActionResult GetCurrentState(string vehicleId)
    {
        var result = _planeStateMachineService.GetPlaneStateMachineCurrentState(vehicleId);
        return result == null? NotFound() : Ok(result);
    }
    
}