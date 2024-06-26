using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Contracts;

public interface IPlaneRepository : IEntityWithIdRepository<PlaneEntity>
{
    public Task<PlaneEntity?> GetByIdWithIncludes(string id);
    Task<PlaneEntity> Create();

    Task<PlaneEntity> UpdateStateAsync(PlaneEntity planeEntity);
    Task<IEnumerable<PlaneEntity>> GetAllPlanes();
}