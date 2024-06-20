using StatelessWithUI.VehicleStateMachines.CarStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Persistence.Domain;

public class CarEntity: VehicleEntityBase
{
    public int HorsePower { get; set; }
    // public CarStateMachine.CarState StateEnum { get; set; }
}