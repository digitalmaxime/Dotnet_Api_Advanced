using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Controllers;

[Controller]
[Route("api/[controller]")]
public class StateController: ControllerBase
{
    private readonly IPlaneStateRepository _planeStateRepository;

    public StateController(IPlaneStateRepository planeStateRepository)
    {
        _planeStateRepository = planeStateRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string id, PlaneStateMachine.PlaneState planeState)
    {
        var result = await _planeStateRepository.GetState(id, planeState);
        
        if (result == null) return NotFound($"Plane State Not Found for stateId : {id}");

        return Ok(result);
    }
    
}