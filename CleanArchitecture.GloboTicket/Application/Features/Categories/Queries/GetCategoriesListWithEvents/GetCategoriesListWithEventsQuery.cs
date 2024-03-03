using MediatR;

namespace Application.Features.Categories.Queries.GetCategoriesListWithEvents;

public class GetCategoriesListWithEventsQuery: IRequest<List<CategoryWithEventDto>>
{
    public bool IncludeHistory { get; set; }
}