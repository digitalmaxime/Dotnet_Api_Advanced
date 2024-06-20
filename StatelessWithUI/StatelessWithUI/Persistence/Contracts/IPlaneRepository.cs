using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneRepository : IEntityWithIdRepository<PlaneSnapshotEntity>
{
}