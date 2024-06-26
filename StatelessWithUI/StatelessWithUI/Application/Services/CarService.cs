using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.VehicleStateMachineFactory;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public class CarService : ICarService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly ICarRepository _carRepository;

    public CarService(IVehicleFactory vehicleFactory, ICarRepository carRepository)
    {
        _vehicleFactory = vehicleFactory;
        _carRepository = carRepository;
    }

    public async Task<IEnumerable<CarEntity>> GetAll()
    {
        return await _carRepository.GetAll();
    }

    public async Task<CarEntity?> CreateAsync(string vehicleId)
    {
        try
        {
            var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
            return new CarEntity()
            {
                Id = stateMachine.VehicleId,
                HorsePower = 0
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
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.CurrentStateName;
    }
    
    public async Task<CarEntity?> GetCarEntity(string vehicleId)
    {
        return await _carRepository.GetById(vehicleId);
    }

    public async Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        return stateMachine.GetPermittedTriggers;
    }

    public void TakeAction(string vehicleId, string action)
    {
        throw new NotImplementedException();
    }

    public void GoToNextState(string vehicleId)
    {
        throw new NotImplementedException();
    }

    public void TakeAction(string vehicleId, PlaneAction action)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Car, vehicleId);
        stateMachine.TakeAction(action);
    }
}