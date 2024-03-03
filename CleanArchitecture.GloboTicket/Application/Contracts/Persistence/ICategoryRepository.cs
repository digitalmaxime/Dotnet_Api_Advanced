using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ICategoryRepository: IAsyncRepository<Category>
{
    Task<List<Category>> GetCategoriesWithEvents(bool requestIncludeHistory);
}