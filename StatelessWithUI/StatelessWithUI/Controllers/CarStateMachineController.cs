using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.CarStateMachine.Commands;
using StatelessWithUI.Application.Features.CarStateMachine.Queries;
using StatelessWithUI.Application.Features.PlaneStateMachine.Commands;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarStateMachineController: ControllerBase
{
    private readonly IMediator _mediator;

    public CarStateMachineController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("car")]
    public async Task<IEnumerable<EntityWithId>> GetCars()
    {
        return await _mediator.Send(new GetAllCarsQuery());
    }

    [HttpGet("car/{id}")]
    public async Task<CarEntity?> GetCar(string id)
    {
        var vehicle = await _mediator.Send(new GetCarByIdQuery(id));
        return vehicle;
    }
        
    [HttpPost("car")]
    public async Task<IActionResult> CreateCar(CarEntity car)
    {
        var success = await _mediator.Send(new CreateCarCommand(car.Id, car.Speed, car.State));
        if (!success)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        return Created();    
    }

    [HttpPost("car/goto-nextstate/{id}")]
    public async Task<IActionResult> GoToNextState(string id)
    {
        await _mediator.Send(new GoToNextCarStateCommand(id));
        return Ok();    
    }
    
    // [HttpPost("vehicle/{id}/begin")]
    // public async Task<IActionResult> StartVehicle(string id)
    // {
    //     var result = await _mediator.Send(new BeginBuildCarCommand() {Id = id});
    //     return result ? Ok() : BadRequest();
    // }
}