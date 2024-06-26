using StatelessWithUI.Controllers;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public interface ITaskService
{
    public Task<BuildTask?> GetBuildTask(string id);
    public Task<BuildTask?> CompleteBuildTask(string id);

    public Task<BuildTask?> CreatePlaneBuildTask(string planeStateId, string taskName);
    Task<GetAllBuildPlaneTaskQueryResponseDto?> GetAllPlaneBuildTasksAsync();
}