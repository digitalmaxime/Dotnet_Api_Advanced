using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.VehicleStateMachineFactory;

public interface IVehicleStateMachineFactory
{
    Task<IVehicleStateMachine> GetOrAddVehicleStateMachine(VehicleType type, string vehicleId);
}