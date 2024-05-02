using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IOrderRepository: IAsyncRepository<Order>
{
    Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size);


    Task<int> GetTotalCountOfOrdersForMonth(DateTime date);
}