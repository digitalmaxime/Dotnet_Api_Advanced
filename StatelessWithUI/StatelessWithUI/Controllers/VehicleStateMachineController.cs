using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.CarStateMachine.Commands;
using StatelessWithUI.Application.Features.CarStateMachine.Queries;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleStateMachineController: ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleStateMachineController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("vehicle")]
    public async Task<IEnumerable<EntityWithId>>? GetCars(VehicleType vehicleType)
    {
        return await _mediator.Send(new GetAllQuery(vehicleType));
    }

    [HttpGet("vehicle/{id}")]
    public async Task<CarEntity?> Get(string id)
    {
        var vehicle = await _mediator.Send(new GetCarByIdQuery(id));
        return vehicle;
    }
        
    [HttpPost("vehicle")]
    public async Task<IActionResult> Create(CarEntity car)
    {
        var success = await _mediator.Send(new CreateCarCommand(car.Id, car.Speed, car.State));
        if (!success)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        return Created();    
    }
    
    // [HttpPost("vehicle/{id}/begin")]
    // public async Task<IActionResult> StartVehicle(string id)
    // {
    //     var result = await _mediator.Send(new BeginBuildCarCommand() {Id = id});
    //     return result ? Ok() : BadRequest();
    // }
}