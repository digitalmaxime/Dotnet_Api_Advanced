using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Contracts;

public interface IEntityWithIdRepository<T> where T : VehicleEntityBase
{
    Task<bool> SaveAsync(T entity);
    Task<T?> GetById(string id);
    Task<List<T>> GetAll();
}