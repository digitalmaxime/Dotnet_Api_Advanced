using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneRepository : EntityWithIdRepository<PlaneEntity>, IPlaneRepository
{
    public PlaneRepository(VehicleDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PlaneEntity?> GetByIdWithIncludes(string id)
    {
        return await _dbContext.PlaneEntity
            .Include(x => x.PlaneStates)
            .ThenInclude(x => x.StateTask)
            // .Include(x => x.InitialStates)
            // .Include(x => x.DesignStates)
            // .Include(x => x.BuildStates)
            // .Include(x => x.TestingStates)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PlaneEntity> Create()
    {
        var planeId = Guid.NewGuid().ToString();

        var createEntity = await _dbContext.PlaneEntity.AddAsync(new PlaneEntity()
        {
            Id = planeId,
            PlaneStates = new List<StateBase>()
        });

        await _dbContext.SaveChangesAsync();
        return createEntity.Entity;
    }

    public async Task<PlaneEntity> UpdateStateAsync(PlaneEntity planeEntity)
    {
        var entity = _dbContext.PlaneEntity.Update(planeEntity);

        if (await _dbContext.SaveChangesAsync() < 1) throw new DbUpdateException();

        return entity.Entity;
    }

    public async Task<IEnumerable<PlaneEntity>> GetAllPlanes()
    {
        var entities = await _dbContext.PlaneEntity
            .Include(x => x.PlaneStates)
            .ThenInclude(x => x.StateTask)
            .ToListAsync();

        return entities;
    }
}