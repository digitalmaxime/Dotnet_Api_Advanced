using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class StateBase
{
    public string Id { get; init; } = null!;

    public string GetStateName() => GetType().Name;
    
    // public string PlaneStateMachineId { get; set; } = null!;
    // public PlaneStateMachine PlaneStateMachine { get; set; }
}