namespace StatelessWithUI.Application.VehicleStateMachines;

public interface IVehicleStateMachine
{
    string VehicleId { get; }
    // public string StateId { get; set; }
    IEnumerable<string> GetPermittedTriggers { get; }
    string CurrentStateName { get; }
    void TakeAction(string actionString);
}