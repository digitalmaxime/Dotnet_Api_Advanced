using MediatR;

namespace Application.Features.Events.Queries.GetEventsList;

public class GetEventsListQuery: IRequest<ICollection<EventDto>>
{
}