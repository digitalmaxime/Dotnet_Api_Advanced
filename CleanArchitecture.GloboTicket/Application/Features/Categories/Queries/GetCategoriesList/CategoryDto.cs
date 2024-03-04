using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoriesList;

public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}