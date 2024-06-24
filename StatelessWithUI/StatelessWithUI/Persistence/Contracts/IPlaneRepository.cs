using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneRepository : IEntityWithIdRepository<PlaneEntity>
{
    public Task<PlaneEntity?> GetByIdWithIncludes(string id);
}