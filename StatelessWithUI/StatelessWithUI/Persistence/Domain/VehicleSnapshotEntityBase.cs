using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public abstract class VehicleSnapshotEntityBase
{
    public string Id { get; init; } = null!;
    public string CurrentStateEnumName { get; set; } = null!;
    public string StateId { get; set; } = null!;
    public StateBase State { get; set; } = null!;
}