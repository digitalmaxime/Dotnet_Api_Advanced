using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class InitialState: StateBase
{
    public override string PlaneEntityId { get; set; } = null!;
    public override PlaneEntity PlaneEntity { get; set; }
}