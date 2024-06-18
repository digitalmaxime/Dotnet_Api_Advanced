using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Persistence.Domain;

public class CarEntity: EntityWithId
{
    public int Speed { get; set; }
    public CarStateMachine.CarState State { get; set; }
}