using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneEntity>> GetAll();
    Task<EntityWithId?> CreateAsync(string vehicleId);
    string GetPlaneState(string vehicleId);
    Task<PlaneEntity?> GetPlaneEntity(string vehicleId);
    Task<IEnumerable<string>> GetPermittedTriggers(string vehicleId);
    void GoToNextState(string vehicleId);
    void TakeAction(string vehicleId, string action);
}