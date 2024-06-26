using StatelessWithUI.Application.Features.Tasks.Queries;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public interface ITaskService
{
    public Task<StateTask?> GetTaskAsync(string id);
    public Task CompleteTaskAsync(string id);

    public Task<StateTask?> CreatePlaneBuildTaskAsync(string planeStateId, string taskName);
    Task<GetAllBuildPlaneTaskQueryResponseDto?> GetAllPlaneBuildTasksAsync();
}