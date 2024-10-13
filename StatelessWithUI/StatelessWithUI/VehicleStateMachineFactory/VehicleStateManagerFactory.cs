using StatelessWithUI.Application.Features.CarStateMachine;
using StatelessWithUI.Application.Features.PlaneStateMachine;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.VehicleStateMachineFactory;

public class VehicleStateMachineStateMachineFactory : IVehicleStateMachineFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Dictionary<string, CarStateMachine> _carStateMachineDictionary = new();
    private readonly Dictionary<string, PlaneStateMachine> _planeStateMachineDictionary = new();

    public VehicleStateMachineStateMachineFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    private CarStateMachine GetOrAddCarStateMachine(string id)
    {
        var success = _carStateMachineDictionary.TryGetValue(id, out var stateMachine);
        if (!success)
        {
            stateMachine = new CarStateMachine(id, _serviceScopeFactory);
            _carStateMachineDictionary.Add(id, stateMachine);
        }

        return stateMachine!;
    }

    private async Task<PlaneStateMachine> GetOrAddPlaneStateMachine(string planeId)
    {
        var success = _planeStateMachineDictionary.TryGetValue(planeId, out var stateMachine);

        if (success) return stateMachine!;
        
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        var plane = await planeStateRepository.GetById(planeId);
        if (plane == null)
        {
            throw new ArgumentException($"Plane not found with id: {planeId}");
        }
            
        stateMachine = new PlaneStateMachine(plane, _serviceScopeFactory);
        _planeStateMachineDictionary.Add(planeId, stateMachine);

        return stateMachine!;
    }

    public async Task<IVehicleStateMachine> GetOrAddVehicleStateMachine(VehicleType type, string vehicleId)
    {
        return type switch
        {
            VehicleType.Car => GetOrAddCarStateMachine(vehicleId),
            VehicleType.Plane => await GetOrAddPlaneStateMachine(vehicleId),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}