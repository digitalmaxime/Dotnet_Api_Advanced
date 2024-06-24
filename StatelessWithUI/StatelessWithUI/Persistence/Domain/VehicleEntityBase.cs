using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public abstract class VehicleEntityBase
{
    public string Id { get; init; } = null!;
    public string CurrentStateEnumName { get; set; } = null!; // TODO: just enum
    // public abstract string GetCurrentStateEnumName(); // TODO:
}