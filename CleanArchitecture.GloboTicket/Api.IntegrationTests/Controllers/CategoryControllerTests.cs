using System.Text.Json;
using Api.IntegrationTest.Base;
using Application.Features.Categories.Queries.GetCategoriesList;

namespace Api.IntegrationTest.Controllers;

public class CategoryControllerTests: IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = _factory.CreateDefaultClient();
        var response = await client.GetAsync("/api/category/all");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<CategoryDto>>(responseString);

        Assert.IsType<List<CategoryDto>>(result);
        Assert.NotEmpty(result);

    }
}