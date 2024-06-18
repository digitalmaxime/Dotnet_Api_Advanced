using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Persistence.Domain;

public class PlaneEntity: EntityWithId
{
    public int Speed { get; set; }
    public PlaneStateMachine.PlaneState State { get; set; }
}