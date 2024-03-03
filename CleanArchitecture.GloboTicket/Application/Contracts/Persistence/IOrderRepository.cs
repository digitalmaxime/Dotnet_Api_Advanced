using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IOrderRepository: IAsyncRepository<Event>
{
    
}