using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Services;

public interface ICarService
{
    Task<IEnumerable<CarEntity>> GetAll();
    Task<CarEntity?> CreateAsync(string vehicleId);
    Task<string> GetCarState(string vehicleId);
    public Task<CarEntity?> GetCarEntity(string vehicleId);

    Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId);
    Task GoToNextState(string vehicleId);
    Task TakeAction(string vehicleId, string action);
}