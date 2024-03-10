using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Events.Queries.GetEventDetail;

public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailDto>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetEventDetailQueryHandler(
        IAsyncRepository<Event> eventRepository, 
        IAsyncRepository<Category> categoryRepository, 
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<EventDetailDto> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        var eventDetailDto = _mapper.Map<EventDetailDto>(@event);

        if (@event != null)
        {
            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

            eventDetailDto.EventDetailCategory = _mapper.Map<EventDetailCategoryDto>(category);
        }

        return eventDetailDto;
    }
}