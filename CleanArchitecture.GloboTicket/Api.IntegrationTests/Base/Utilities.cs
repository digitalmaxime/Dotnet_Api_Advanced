using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Api.IntegrationTest.Base;

public static class Utilities
{
    public static void InitializeDbForTests(GloboTicketDbContext dbContext)
    {
        var concertGuild = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35");
        var musicalGuid = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96");
        var playGuid = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450");
        var conferenceGuid = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09");

        dbContext.Categories.AddRange(
            new Category { CategoryId = concertGuild, Name = "Concerts" },
            new Category { CategoryId = musicalGuid, Name = "Musicals" },
            new Category { CategoryId = playGuid, Name = "Plays" },
            new Category { CategoryId = conferenceGuid, Name = "Conferences" }
        );

        dbContext.SaveChangesAsync();
    }
}