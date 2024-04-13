using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> IsEventNameAndDateUnique(string eventName, DateTime eventDate)
    {
        return Task.FromResult(DbContext.Events.Any(e => e.Name.Equals(eventName) && e.Date.Date.Equals(eventDate)));
    }
}