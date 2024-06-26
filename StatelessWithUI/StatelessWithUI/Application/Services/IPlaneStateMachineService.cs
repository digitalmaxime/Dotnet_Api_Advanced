using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;

namespace StatelessWithUI.Application.Services;

public interface IPlaneStateMachineService
{
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    Task<bool> TakeActionAsync(string vehicleId, PlaneAction action);
    string? GetPlaneStateMachineCurrentState(string vehicleId);

}