using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class StateBase
{
    public string Id { get; init; } = null!;

    protected string GetStateName() => GetType().Name;
}