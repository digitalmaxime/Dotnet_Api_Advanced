using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.VehicleStateMachineFactory;

public interface IVehicleFactory
{
    IVehicleStateMachine CreateVehicleStateMachine(VehicleType type, string vehicleId);
}