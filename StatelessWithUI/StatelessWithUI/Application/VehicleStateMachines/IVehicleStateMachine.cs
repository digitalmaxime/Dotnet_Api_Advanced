using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;

namespace StatelessWithUI.Application.VehicleStateMachines;

public interface IVehicleStateMachine
{
    string VehicleId { get; }
    IEnumerable<string> GetPermittedTriggers { get; }
    string CurrentStateName { get; }
    void TakeAction(PlaneAction action);
}