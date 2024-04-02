using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

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
        
        // seed data, added through migrations
        var concertGuid = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35");
        var musicalGuid = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96");
        var playGuid = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450");
        var conferenceGuid = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09");
        
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = concertGuid, Name = "Concerts" },
            new Category { CategoryId = musicalGuid, Name = "Musicals" },
            new Category { CategoryId = playGuid, Name = "Plays" },
            new Category { CategoryId = conferenceGuid, Name = "Conferences" }
        );

        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
            Name = "John Egbert Live",
            Date = DateTime.Now.AddMonths(6),
            Price = 30,
            Artist = "John Egbert",
            CategoryId = concertGuid,
            ImageUrl = "https://www.ticketmaster.com.au/John-Egbert-tickets/artist/12345",
            Description = "John Egbert is back in town!"
        });
        
        modelBuilder.Entity<Event>().HasData(new Event
        {
            EventId = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
            Name = "The State of React 2021",
            Date = DateTime.Now.AddMonths(9),
            Price = 30,
            Artist = "ReactJS",
            CategoryId = conferenceGuid,
            ImageUrl = "https://www.reactjs.org",
            Description = "The State of React 2021 is a conference that brings in the best React developers from around the world."
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(ct);
    }
}