using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;

namespace StatelessWithUI.Application.Features.CarStateMachine.Services;

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
            var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
            return new CarEntity()
            {
                Id = stateMachine.EntityId, 
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

    public async Task<string> GetCarState(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetCurrentState;
    }
    
    public async Task<CarEntity?> GetCarEntity(string vehicleId)
    {
        return await _carStateRepository.GetById(vehicleId);
    }

    public async Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetPermittedTriggers;
    }

    public async Task GoToNextState(string vehicleId)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.GoToNextState();
    }

    public async Task TakeAction(string vehicleId, string action)
    {
        var stateMachine = await _vehicleStateMachineFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.TakeAction(action);
    }
}