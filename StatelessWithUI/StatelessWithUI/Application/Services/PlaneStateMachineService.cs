using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.VehicleStateMachineFactory;
using StatelessWithUI.Persistence.Constants;

namespace StatelessWithUI.Application.Services;

public class PlaneStateMachineService: IPlaneStateMachineService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly IPlaneRepository _planeRepository;

    public PlaneStateMachineService(IVehicleFactory vehicleFactory, IPlaneRepository planeRepository)
    {
        _vehicleFactory = vehicleFactory;
        _planeRepository = planeRepository;
    }


    public async Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId)
    {
        if (await _planeRepository.GetById(vehicleId) == null) return null;

        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.GetPermittedTriggers;
    }

    public async Task<bool> TakeActionAsync(string vehicleId, string action)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);

        if (stateMachine == null) return false;

        try
        {
            stateMachine.TakeAction(action);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    
    public string? GetPlaneStateMachineCurrentState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.CurrentStateName;
    }
}