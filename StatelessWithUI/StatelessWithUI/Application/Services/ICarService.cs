using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface ICarService
{
    Task<IEnumerable<CarVehicleEntity>> GetAll();
    Task<CarVehicleEntity?> CreateAsync(string vehicleId);
    string GetCarState(string vehicleId);
    public Task<CarVehicleEntity?> GetCarEntity(string vehicleId);

    Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId);
    void GoToNextState(string vehicleId);
    void TakeAction(string vehicleId, string action);
}