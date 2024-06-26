using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Contracts;

public interface ICarRepository : IEntityWithIdRepository<CarEntity>
{
}