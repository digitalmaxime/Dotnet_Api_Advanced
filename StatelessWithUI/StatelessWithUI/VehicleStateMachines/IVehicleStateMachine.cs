using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.VehicleStateMachines;

public interface IVehicleStateMachine
{
    string Id { get; }
    IEnumerable<string> GetPermittedTriggers { get; }
    string CurrentStateName { get; }
    Enum State { get; }
    void TakeAction(string actionString);
    void GoToNextState();
}