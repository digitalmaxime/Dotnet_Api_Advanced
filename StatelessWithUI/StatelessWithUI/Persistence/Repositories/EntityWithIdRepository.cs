using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class EntityWithIdRepository<T> : IEntityWithIdRepository<T> where T : VehicleEntityBase
{
    protected readonly VehicleDbContext _dbContext;

    protected EntityWithIdRepository(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveAsync(T entity)
    {
        var vehicleEntity =
            await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (vehicleEntity == null)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
        else
        {
            _dbContext.Set<T>().Update(entity);
        }

        try
        {
            var n = await _dbContext.SaveChangesAsync();
            return n > 0;
        }
        catch (DbUpdateConcurrencyException e)
        {
            Console.WriteLine(e.Message);
            throw new Exception("Concurrency exception : " + e.Message);
        }
    }

    public async Task<T?> GetById(string id)
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}