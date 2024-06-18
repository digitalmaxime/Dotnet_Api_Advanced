using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.CarStateMachine.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleStateMachineController
{
    private readonly IMediator _mediator;

    public VehicleStateMachineController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("vehicle")]
    public async Task<IEnumerable<CarEntity>>? Get()
    {
        var vehicles = await _mediator.Send(new GetAllCarQuery());
        return vehicles == null ? null : vehicles;
    }
    
    [HttpGet("vehicle/{id}")]
    public async Task<CarEntity?> Get(string id)
    {
        var vehicle = await _mediator.Send(new GetCarByIdQuery(id));
        return vehicle;
    }
    
    // [HttpPost("vehicle/{id}/begin")]
    // public async Task<IActionResult> StartVehicle(string id)
    // {
    //     var result = await _mediator.Send(new BeginBuildCarCommand() {Id = id});
    //     return result ? Ok() : BadRequest();
    // }
}