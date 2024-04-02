using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Events.Queries.GetEventsList;

public class GetEventListQueryHandler: IRequestHandler<GetEventsListQuery, ICollection<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventListQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
    
    public async Task<ICollection<EventDto>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
    {
        var events = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
        
        return _mapper.Map<ICollection<EventDto>>(events);
    }
}