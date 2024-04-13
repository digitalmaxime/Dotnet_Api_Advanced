using Application.Contracts.Api;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.Contexts;
using Shouldly;

namespace Persistence.IntegrationTest;

public class UnitTest1
{
    private readonly GloboTicketDbContext _globoTicketDbContext;
    private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;

    public UnitTest1()
    {
        var dbContextOptions = new DbContextOptionsBuilder<GloboTicketDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = Guid.NewGuid().ToString();
        _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        _loggedInUserServiceMock.Setup(x => x.UserId).Returns(_loggedInUserId);
        _globoTicketDbContext = new GloboTicketDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    [Fact]
    public async void Test1()
    {
        var evt = new Event() { EventId = Guid.NewGuid(), Name = "Test event" };
        _globoTicketDbContext.Events.Add(evt);
        await _globoTicketDbContext.SaveChangesAsync();
        evt.CreatedBy.ShouldBe(_loggedInUserId);
    }
}