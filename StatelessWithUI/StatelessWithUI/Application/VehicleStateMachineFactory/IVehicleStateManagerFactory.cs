using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.VehicleStateMachineFactory;

public interface IVehicleFactory
{
    IVehicleStateMachine GetOrAddVehicleStateMachine(VehicleType type, string vehicleId);
    IVehicleStateMachine? GetVehicleStateMachine(VehicleType vehicleType, string vehicleId);
}