using Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController: ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/getpageordersformonth", Name = "GetPagedOrdersForMonth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<PagedOrdersForMonthVm>> GetPagedOrdersForMonth(DateTime date, int page, int size)
    {
        var getOrdersForMonthQuery = new GetOrdersForMonthQuery(date, page, size);
        var dtos = await _mediator.Send(getOrdersForMonthQuery);

        return Ok(dtos);
    }
}