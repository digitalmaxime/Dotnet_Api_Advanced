using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class GloboTickerDbContext : DbContext
{
    public GloboTickerDbContext(DbContextOptions<GloboTickerDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GloboTickerDbContext).Assembly);
    }
}