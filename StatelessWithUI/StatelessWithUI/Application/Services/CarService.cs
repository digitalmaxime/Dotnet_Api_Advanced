using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Application.Services;

public class CarService : ICarService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly ICarStateRepository _carStateRepository;

    public CarService(IVehicleFactory vehicleFactory, ICarStateRepository carStateRepository)
    {
        _vehicleFactory = vehicleFactory;
        _carStateRepository = carStateRepository;
    }

    public async Task<IEnumerable<CarEntity>> GetAll()
    {
        return await _carStateRepository.GetAll();
    }

    public async Task<bool> CreateAsync(string vehicleId)
    {
        try
        {
            _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public string GetCarState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetCurrentState;
    }
    
    public async Task<CarEntity?> GetCarEntity(string vehicleId)
    {
        return await _carStateRepository.GetById(vehicleId);
    }

    public IEnumerable<string> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetPermittedTriggers;
    }

    public void GoToNextState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        var nextAvailableStates = stateMachine.GetPermittedTriggers;
        IEnumerable<string> availableStates = nextAvailableStates as string[] ?? nextAvailableStates.ToArray();
        if (availableStates.Any())
        {
            stateMachine.TakeAction(availableStates.First());
        }
    }

    public void TakeAction(string vehicleId, string action)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.TakeAction(action);
    }
}