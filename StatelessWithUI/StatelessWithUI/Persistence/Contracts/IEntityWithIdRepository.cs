using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Contracts;

public interface IEntityWithIdRepository<T> where T : EntityBase
{
    Task<bool> SaveAsync(T entity);
    Task<T?> GetById(string id);
    Task<List<T>> GetAll();
}