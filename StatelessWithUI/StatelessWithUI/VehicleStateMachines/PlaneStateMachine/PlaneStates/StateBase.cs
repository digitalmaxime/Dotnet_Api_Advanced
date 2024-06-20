using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class StateBase
{
    public string Id { get; init; } = null!;

    public string GetStateName() => GetType().Name;
}