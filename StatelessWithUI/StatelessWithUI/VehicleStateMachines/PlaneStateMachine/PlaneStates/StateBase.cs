using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class StateBase
{
    public string Id { get; init; } = null!;

    public string GetStateName() => GetType().Name;
    public virtual string PlaneEntityId { get; set; } = null!;
    public virtual PlaneEntity PlaneEntity { get; set; }

}