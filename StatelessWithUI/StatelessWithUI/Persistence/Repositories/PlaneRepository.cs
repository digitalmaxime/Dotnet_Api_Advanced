using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneRepository : EntityWithIdRepository<PlaneEntity>, IPlaneRepository
{
    public PlaneRepository(VehicleDbContext dbContext): base(dbContext)
    {
    }

    public async Task<PlaneEntity?> GetByIdWithIncludes(string id)
    {
        return await _dbContext.PlaneEntity
            .Include(x => x.InitialStates)
            .Include(x => x.DesignStates)
            .Include(x => x.BuildStates)
            .Include(x => x.TestingStates)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}