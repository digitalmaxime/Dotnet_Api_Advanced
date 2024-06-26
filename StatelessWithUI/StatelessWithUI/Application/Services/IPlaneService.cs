using System.Numerics;
using StatelessWithUI.Application.Features.Plane.Queries;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Services;

public interface  IPlaneService
{
    Task<IEnumerable<PlaneEntity>> GetAllPlanes();
    Task<PlaneEntity?> CreatePlaneAtInitialStateAsync();
    Task<PlaneEntity?> CreatePlaneAtBuildStateAsync();
    Task<GetPlaneQueryResponseDto?> GetPlaneEntity(string vehicleId, bool includes = false);

    Task<PlaneEntity> InitializeStates(string planeEnityId);
}