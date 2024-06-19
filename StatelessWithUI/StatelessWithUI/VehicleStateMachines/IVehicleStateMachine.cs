using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.VehicleStateMachines;

public interface IVehicleStateMachine
{
    string Id { get; }
    IEnumerable<string> GetPermittedTriggers { get; }
    string CurrentState { get; }
    VehicleStateBase State { get; }
    void TakeAction(string actionString);
    void GoToNextState();
}