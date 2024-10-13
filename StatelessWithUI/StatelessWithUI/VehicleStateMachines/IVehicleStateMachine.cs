using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines;

public interface IVehicleStateMachine
{
    public string EntityId { get; }

    IEnumerable<string> GetPermittedTriggers { get; }
    string GetCurrentState { get; }
    void TakeAction(string actionString);
    void GoToNextState();
}