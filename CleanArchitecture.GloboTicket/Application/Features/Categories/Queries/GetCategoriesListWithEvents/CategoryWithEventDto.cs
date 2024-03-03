namespace Application.Features.Categories.Queries.GetCategoriesListWithEvents;

public class CategoryWithEventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<CategoryEventDto>? EventDtos { get; set; } = default!;
}