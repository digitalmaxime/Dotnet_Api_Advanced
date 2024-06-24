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
public class CarController: ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("car")]
    public async Task<IEnumerable<VehicleEntityBase>> GetCars()
    {
        return await _mediator.Send(new GetAllCarsQuery());
    }

    [HttpGet("car/{id}")]
    public async Task<CarEntity?> Get(string id)
    {
        var vehicle = await _mediator.Send(new GetCarByIdQuery(id));
        return vehicle;
    }
        
    [HttpPost("car")]
    public async Task<IActionResult> Create(CarEntity car)
    {
        var createdCar = await _mediator.Send(new CreateCarCommand(car.Id));
        if (createdCar == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        return CreatedAtAction(nameof(Get), new { id = createdCar.Id }, createdCar);
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