namespace StatelessWithUI.Application.Services;

public interface IPlaneStateMachineService
{
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    Task<bool> TakeActionAsync(string vehicleId, string action);
    string? GetPlaneStateMachineCurrentState(string vehicleId);

}