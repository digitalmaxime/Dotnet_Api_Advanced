using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Services;

public class PlaneService : IPlaneService
{
    private readonly IVehicleStateMachineFactory _vehicleStateMachineFactory;
    private readonly IPlaneStateRepository _planeStateRepository;

    public PlaneService(IVehicleStateMachineFactory vehicleStateMachineFactory, IPlaneStateRepository planeStateRepository)
    {
        _vehicleStateMachineFactory = vehicleStateMachineFactory;
        _planeStateRepository = planeStateRepository;
    }

    public async Task<IEnumerable<PlaneEntity>> GetAll()
    {
        return await _planeStateRepository.GetAll();
    }

    public async Task<EntityWithId?> CreateAsync(string vehicleId)
    {
        try
        {
            var plane = await _planeStateRepository.GetById(vehicleId);
            if (plane != null)
            {
                return plane;
            }

            var newPlane = new PlaneEntity()
            {
                Id = vehicleId, State = PlaneStateMachine.PlaneState.Stopped, Speed = 0
            };

            var success = await _planeStateRepository.Save(newPlane);
            
            if (!success)
            {
                return null;
            }
            
            await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
            return newPlane;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<string> GetPlaneState(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine.GetCurrentState;
    }

    public async Task<PlaneEntity?> GetPlaneEntity(string vehicleId)
    {
        return await _planeStateRepository.GetById(vehicleId);
    }

    public async Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine.GetPermittedTriggers;
    }

    public async void GoToNextState(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        stateMachine.GoToNextState();
    }

    public async void TakeAction(string vehicleId, string action)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        stateMachine.TakeAction(action);
    }
}