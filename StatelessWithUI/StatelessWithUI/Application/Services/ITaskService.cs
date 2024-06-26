using StatelessWithUI.Application.Features.Tasks.Queries;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public interface ITaskService
{
    public Task<StateTask?> GetBuildTask(string id);
    public Task<StateTask?> CompleteBuildTask(string id);

    public Task<StateTask?> CreatePlaneBuildTask(string planeStateId, string taskName);
    Task<GetAllBuildPlaneTaskQueryResponseDto?> GetAllPlaneBuildTasksAsync();
}