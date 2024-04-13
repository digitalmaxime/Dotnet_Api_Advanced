using API.Utility;
using Application.Features.Events.Commands.CreateEvent;
using Application.Features.Events.Commands.DeleteEvent;
using Application.Features.Events.Commands.UpdateEvent;
using Application.Features.Events.Queries.GetEventDetail;
using Application.Features.Events.Queries.GetEventsExport;
using Application.Features.Events.Queries.GetEventsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetAllEvents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<EventDto>>> GetAllEvents()
    {
        var response = await _mediator.Send(new GetEventsListQuery());
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpGet("{id}", Name = "GetEventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventDetailDto>> GetEventById(Guid id)
    {
        var query = new GetEventDetailQuery() { Id = id };
        var response = await _mediator.Send(query);
        if (response != null)
        {
            return Ok(response);
        }

        return BadRequest();
    }

    [HttpPost(Name = "AddEvent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> AddEvent([FromBody] CreateEventCommand createEventCommand)
    {
        var response = await _mediator.Send(createEventCommand);
        if (response != Guid.Empty)
        {
            return CreatedAtRoute("GetEventById", new { id = response }, response);
        }

        return BadRequest();
    }

    [HttpPut(Name = "UpdateEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateEvent([FromBody] UpdateEventCommand updateEventCommand)
    {
        await _mediator.Send(updateEventCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteEvent")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteEvent(Guid id)
    {
        await _mediator.Send(new DeleteEventCommand() { EventId = id });
        return NoContent();
    }

    [HttpGet("export", Name = "ExportEvents")]
    [FileResultContentType("text/csv")]
    public async Task<ActionResult<FileResult>> ExportEvents()
    {
        var fileDto = await _mediator.Send(new GetEventsExportQuery());

        if (fileDto == null || fileDto.Data == null)
        {
            // return PhysicalFile()
            // return Problem("Error exporting events", null, StatusCodes.Status500InternalServerError,
            //     "Error exporting events");
            return Problem("Error exporting events");
        }

        return Ok(File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName));
    }
}