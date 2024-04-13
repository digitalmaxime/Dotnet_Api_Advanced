using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderRepository: BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }
}