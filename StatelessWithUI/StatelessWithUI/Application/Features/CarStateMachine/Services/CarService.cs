using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Application.Services;

public class CarService : ICarService
{
    private readonly IVehicleStateMachineFactory _vehicleStateMachineFactory;
    private readonly ICarStateRepository _carStateRepository;

    public CarService(IVehicleStateMachineFactory vehicleStateMachineFactory, ICarStateRepository carStateRepository)
    {
        _vehicleStateMachineFactory = vehicleStateMachineFactory;
        _carStateRepository = carStateRepository;
    }

    public async Task<IEnumerable<CarEntity>> GetAll()
    {
        return await _carStateRepository.GetAll();
    }

    public async Task<CarEntity?> CreateAsync(string vehicleId)
    {
        try
        {
            var stateMachine = _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
            return new CarEntity()
            {
                Id = stateMachine.Id, 
                State = Enum.Parse<CarStateMachine.CarState>(stateMachine.GetCurrentState),
                Speed = 0
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public string GetCarState(string vehicleId)
    {
        var stateMachine = _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetCurrentState;
    }
    
    public async Task<CarEntity?> GetCarEntity(string vehicleId)
    {
        return await _carStateRepository.GetById(vehicleId);
    }

    public async Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetPermittedTriggers;
    }

    public void GoToNextState(string vehicleId)
    {
        var stateMachine = _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.GoToNextState();
    }

    public void TakeAction(string vehicleId, string action)
    {
        var stateMachine = _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.TakeAction(action);
    }
}