using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.CarStateMachine.Commands;
using StatelessWithUI.Application.Features.CarStateMachine.Queries;
using StatelessWithUI.Application.Features.PlaneStateMachine.Commands;
using StatelessWithUI.Persistence.Constants;
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

    /*
    [HttpGet("vehicle")]
    public async Task<IEnumerable<EntityWithId>>? GetCars()
    {
        return await _mediator.Send(new GetAllCarsQuery(vehicleType));
    }
*/
    [HttpGet("vehicle/car/{id}")]
    public async Task<CarEntity?> GetCar(string id)
    {
        var vehicle = await _mediator.Send(new GetCarByIdQuery(id));
        return vehicle;
    }

    [HttpPost("vehicle/car")]
    public async Task<IActionResult> CreateCar(CarEntity car)
    {
        var success = await _mediator.Send(new CreateCarCommand(car.Id, car.Speed, car.State));
        if (!success)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Created();
    }

    [HttpPost("vehicle/plane")]
    public async Task<IActionResult> CreatePlane(PlaneEntity plane)
    {
        var success = await _mediator.Send(new CreatePlaneCommand(plane.Id, plane.Speed, plane.State));
        if (!success)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Created();
    }

    /*
    [HttpPost("car/nextstate/{id}")]
    public async Task<IActionResult> GoToNextState(string id)
    {
        var success = await _mediator.Send(new GoToNextCarStateCommand(id));
        if (!success)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        return Ok();    
    }
    */
    // [HttpPost("vehicle/{id}/begin")]
    // public async Task<IActionResult> StartVehicle(string id)
    // {
    //     var result = await _mediator.Send(new BeginBuildCarCommand() {Id = id});
    //     return result ? Ok() : BadRequest();
    // }
}