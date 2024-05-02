using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEventCommandHandler> _logger;

    public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper,
        ILogger<UpdateEventCommandHandler> logger)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);


        if (eventToUpdate == null)
        {
            _logger.LogError("No event found with EventId : {eventId}", request.EventId);
            throw new NotFoundException(nameof(Event), request.EventId);
        }
        else
        {
            _mapper.Map<UpdateEventCommand, Event>(request, eventToUpdate);

            await _eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}