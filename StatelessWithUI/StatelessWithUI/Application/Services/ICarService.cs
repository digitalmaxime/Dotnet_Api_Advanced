using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface ICarService
{
    Task<IEnumerable<CarSnapshotEntity>> GetAll();
    Task<CarSnapshotEntity?> CreateAsync(string vehicleId);
    string GetCarState(string vehicleId);
    public Task<CarSnapshotEntity?> GetCarEntity(string vehicleId);

    Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId);
    void TakeAction(string vehicleId, string action);
}