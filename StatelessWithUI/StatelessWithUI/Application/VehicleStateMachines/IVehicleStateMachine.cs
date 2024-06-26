using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.VehicleStateMachines;

public interface IVehicleStateMachine
{
    string Id { get; }
    // public string StateId { get; set; }
    IEnumerable<string> GetPermittedTriggers { get; }
    string CurrentStateName { get; }
    Enum StateEnum { get; }
    void TakeAction(string actionString);
}