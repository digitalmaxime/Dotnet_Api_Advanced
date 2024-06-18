using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface ICarService
{
    Task<IEnumerable<CarEntity>> GetAll();
    Task<bool> CreateAsync(string vehicleId);
    string GetCarState(string vehicleId);
    public Task<CarEntity?> GetCarEntity(string vehicleId);

    IEnumerable<string> GetPermittedTriggers(string vehicleId);
    void GoToNextState(string vehicleId);
    void TakeAction(string vehicleId, string action);
}