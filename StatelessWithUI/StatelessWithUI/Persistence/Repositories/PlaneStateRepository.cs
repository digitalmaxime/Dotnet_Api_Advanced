using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneStateRepository : EntityWithIdRepository<PlaneEntity>, IPlaneStateRepository // TODO: valider EntityWithIdRepository dans l'interface IPlaneStateRepository
{
    public PlaneStateRepository(VehicleDbContext dbContext): base(dbContext)
    {
    }
}