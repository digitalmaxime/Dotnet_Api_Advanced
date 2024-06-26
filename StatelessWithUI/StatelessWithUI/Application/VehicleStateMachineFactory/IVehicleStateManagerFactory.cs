using StatelessWithUI.Application.VehicleStateMachines;
using StatelessWithUI.Persistence.Constants;

namespace StatelessWithUI.Application.VehicleStateMachineFactory;

public interface IVehicleFactory
{
    IVehicleStateMachine GetOrAddVehicleStateMachine(VehicleType type, string vehicleId);
    IVehicleStateMachine? GetVehicleStateMachine(VehicleType vehicleType, string vehicleId);
}