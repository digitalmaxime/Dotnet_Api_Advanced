namespace Application.Contracts.Api;

public interface ILoggedInUserService
{
    public string? UserId { get; }
}