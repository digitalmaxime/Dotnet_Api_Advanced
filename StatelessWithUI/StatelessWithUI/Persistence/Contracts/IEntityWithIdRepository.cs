using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Contracts;

public interface IEntityWithIdRepository<T> where T : EntityWithId
{
    Task Save(T entity);
    Task<T?> GetById(string id);
}