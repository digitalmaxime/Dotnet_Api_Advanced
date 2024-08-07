using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface ICarService
{
    Task<IEnumerable<CarEntity>> GetAll();
    Task<CarEntity?> CreateAsync(string vehicleId);
    string GetCarState(string vehicleId);
    public Task<CarEntity?> GetCarEntity(string vehicleId);

    Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId);
    void GoToNextState(string vehicleId);
    void TakeAction(string vehicleId, string action);
}