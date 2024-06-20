using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneRepository : EntityWithIdRepository<PlaneEntity>, IPlaneRepository
{
    public PlaneRepository(VehicleDbContext dbContext): base(dbContext)
    {
    }
}