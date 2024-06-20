using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneSnapshotEntity>> GetAll();
    Task<VehicleSnapshotEntityBase?> CreateAsync(string vehicleId);
    string GetPlaneState(string vehicleId);
    Task<PlaneSnapshotEntity?> GetPlaneEntity(string vehicleId);
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    Task<bool> TakeAction(string vehicleId, string action);
}