using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Category>> GetCategoriesWithEvents(bool requestIncludeHistory)
    {
        var allCategories = DbContext.Categories.Include(c => c.Events).AsQueryable();

        if (!requestIncludeHistory)
        {
            await allCategories.ForEachAsync(p => p.Events?.ToList().RemoveAll(evt => evt.Date < DateTime.Today));
        } 
        
        return await allCategories.ToListAsync();
    }
}