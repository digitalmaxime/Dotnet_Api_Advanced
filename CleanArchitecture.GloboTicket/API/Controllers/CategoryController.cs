using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Queries.GetCategoriesList;
using Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all", Name = "GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> GetAllCategories()
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
    {
        var response = await _mediator.Send(new GetCategoriesListQuery());
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpGet("allWithEvents", Name = "GetAllCategoriesWithEvents")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryWithEventDto>> GetAllCategoriesWithEvents(bool includeHistory)
    {
        var query = new GetCategoriesListWithEventsQuery() { IncludeHistory = includeHistory };
        var response = await _mediator.Send(query);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }
    
    [HttpPost(Name = "AddCategory")]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        var response = await _mediator.Send(createCategoryCommand);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }
}