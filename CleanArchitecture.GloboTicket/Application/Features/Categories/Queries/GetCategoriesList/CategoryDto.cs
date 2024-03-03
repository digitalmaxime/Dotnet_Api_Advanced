namespace Application.Features.Categories.Queries.GetCategoriesList;

public record CategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}