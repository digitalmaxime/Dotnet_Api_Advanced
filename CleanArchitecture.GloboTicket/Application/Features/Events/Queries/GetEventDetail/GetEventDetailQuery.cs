using MediatR;

namespace Application.Features.Events.Queries.GetEventDetail;

public class GetEventDetailQuery: IRequest<EventDetailDto>
{
    public Guid Id { get; set; }
}