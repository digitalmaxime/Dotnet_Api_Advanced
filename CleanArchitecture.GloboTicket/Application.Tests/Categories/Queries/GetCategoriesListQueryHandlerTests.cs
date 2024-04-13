using Application.Contracts.Persistence;
using Application.Features.Categories.Queries.GetCategoriesList;
using Application.Profiles;
using AutoMapper;
using Domain.Entities;
using Moq;
using Shouldly;

namespace ApplicationTests.Categories.Queries;

public class GetCategoriesListQueryHandlerTests
{
    [Fact]
    public async Task GetCategoriesListTest()
    {
        // Arrange
        var driver = new GetCategoriesListQueryHandlerDriver();
        var categories = new List<Category>()
        {
            new () {
                CategoryId = Guid.NewGuid(),
                Name = "name1"
            },
            new () {
                CategoryId = Guid.NewGuid(),
                Name = "name2"
            },
        };
        
        driver.SetupCategoryRepositoryListAllAsync(categories);
        
        var handler = driver.Build();
    
        // Act
        var result = await handler.Handle(new GetCategoriesListQuery(), CancellationToken.None);
    
        // Assert
        result.ShouldBeOfType<List<CategoryDto>>();
        result.Count.ShouldBe(2);
    }
    
    private class GetCategoriesListQueryHandlerDriver
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
    
        public GetCategoriesListQueryHandlerDriver()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
    
            var configProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = configProvider.CreateMapper();
        }
    
        public GetCategoriesListQueryHandlerDriver SetupCategoryRepositoryListAllAsync(IReadOnlyList<Category> res)
        {
            _mockCategoryRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(res);
            return this;
        }
    
        public GetCategoriesListQueryHandlerDriver SetupCategoryRepositoryAddAsync(Category res)
        {
            _mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync(res);
            return this;
        }
    
        public GetCategoriesListQueryHandler Build()
        {
            return new GetCategoriesListQueryHandler(_mockCategoryRepository.Object, _mapper);
        }
    }
}