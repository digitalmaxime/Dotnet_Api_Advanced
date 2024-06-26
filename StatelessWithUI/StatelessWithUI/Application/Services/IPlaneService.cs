using System.Numerics;
using StatelessWithUI.Application.Features.Plane.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneEntity>> GetAllPlanes();
    Task<PlaneEntity?> CreatePlaneAtInitialStateAsync();
    Task<PlaneEntity?> CreatePlaneAtBuildStateAsync();
    string? GetPlaneState(string vehicleId);
    Task<GetPlaneQueryResponseDto?> GetPlaneEntity(string vehicleId, bool includes = false);
    Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId);
    Task<bool> TakeActionAsync(string vehicleId, string action);
    Task<PlaneEntity> InitializeStates(string planeEnityId);
}