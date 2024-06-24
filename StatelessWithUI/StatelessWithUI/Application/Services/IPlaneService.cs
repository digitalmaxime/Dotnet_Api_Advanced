using StatelessWithUI.Application.Features.PlaneStateMachine.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneEntity>> GetAll();
    Task<VehicleEntityBase?> CreateAsync(string vehicleId);
    string GetPlaneState(string vehicleId);
    Task<GetPlaneQueryResponseDto?> GetPlaneEntity(string vehicleId, bool includes = false);
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    Task<bool> TakeAction(string vehicleId, string action);
}