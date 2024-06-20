using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public abstract class VehicleEntityBase
{
    public string Id { get; init; } = null!;
    public string StateId { get; set; }
    public StateBase State { get; set; }
}