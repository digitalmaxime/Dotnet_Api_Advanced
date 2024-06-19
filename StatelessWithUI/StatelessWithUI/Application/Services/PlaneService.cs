using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;

namespace StatelessWithUI.Application.Services;

public class PlaneService : IPlaneService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly IPlaneStateRepository _planeStateRepository;

    public PlaneService(IVehicleFactory vehicleFactory, IPlaneStateRepository planeStateRepository)
    {
        _vehicleFactory = vehicleFactory;
        _planeStateRepository = planeStateRepository;
    }

    public async Task<IEnumerable<PlaneEntity>> GetAll()
    {
        return await _planeStateRepository.GetAll();
    }

    public async Task<EntityBase?> CreateAsync(string vehicleId)
    {
        try
        {
            var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
            return new PlaneEntity()
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

    public async Task<PlaneEntity?> GetPlaneEntity(string vehicleId)
    {
        return await _planeStateRepository.GetById(vehicleId);
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