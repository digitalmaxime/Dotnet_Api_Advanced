using System.Security.Claims;
using API.ServiceCollectionExtensions;
using Application.Contracts.Api;

namespace API.Services;

public class LoggedInUserService: ILoggedInUserService
{
    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string? UserId { get; }
}