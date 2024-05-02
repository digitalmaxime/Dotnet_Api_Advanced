using System.Runtime.InteropServices.JavaScript;
using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderRepository: BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
    {
        return await DbContext.Orders.Where(x => x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year)
            .Skip((page - 1) * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
    {
        return await DbContext.Orders.CountAsync(x =>
            x.OrderPlaced.Month == date.Month && x.OrderPlaced.Year == date.Year);
    }
}