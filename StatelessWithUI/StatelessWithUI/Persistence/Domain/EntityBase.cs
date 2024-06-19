using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Persistence.Domain;

public abstract class EntityBase
{
    public string Id { get; init; } = null!;
    public string StateId { get; set; }
    public VehicleStateBase State { get; set; }
}