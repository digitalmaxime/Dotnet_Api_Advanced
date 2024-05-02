using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Events.Queries.GetEventsList;

public class GetEventListQueryHandler : IRequestHandler<GetEventsListQuery, ICollection<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEventListQueryHandler> _logger;

    public GetEventListQueryHandler(
        IEventRepository eventRepository,
        IMapper mapper,
        ILogger<GetEventListQueryHandler> logger)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ICollection<EventDto>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
    {
        var events = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);

        return _mapper.Map<ICollection<EventDto>>(events);
    }
}