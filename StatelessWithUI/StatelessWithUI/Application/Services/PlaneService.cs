using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;

namespace StatelessWithUI.Application.Services;

public class PlaneService : IPlaneService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly IPlaneRepository _planeRepository;

    public PlaneService(IVehicleFactory vehicleFactory, IPlaneRepository planeRepository)
    {
        _vehicleFactory = vehicleFactory;
        _planeRepository = planeRepository;
    }

    public async Task<IEnumerable<PlaneVehicleEntity>> GetAll()
    {
        return await _planeRepository.GetAll();
    }

    public async Task<VehicleEntityBase?> CreateAsync(string vehicleId)
    {
        try
        {
            var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
            return new PlaneVehicleEntity()
            {
                Id = stateMachine.Id,
                State = stateMachine.State
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public string GetPlaneState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine.CurrentState;
    }

    public async Task<PlaneVehicleEntity?> GetPlaneEntity(string vehicleId)
    {
        return await _planeRepository.GetById(vehicleId);
    }

    public async Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.GetPermittedTriggers;
    }

    public void GoToNextState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        stateMachine?.GoToNextState();
    }

    public async Task<bool> TakeAction(string vehicleId, string action)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);

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
}