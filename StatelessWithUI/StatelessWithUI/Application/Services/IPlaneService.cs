using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneEntity>> GetAll();
    Task<VehicleEntityBase?> CreateAsync(string vehicleId);
    string GetPlaneState(string vehicleId);
    Task<PlaneEntity?> GetPlaneEntity(string vehicleId);
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    void GoToNextState(string vehicleId);
    Task<bool> TakeAction(string vehicleId, string action);
}