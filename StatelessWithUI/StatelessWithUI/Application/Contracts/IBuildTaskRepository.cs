using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Contracts;

public interface IBuildTaskRepository
{
    public Task<StateTask?> GetTaskByIdAsync(string id);
    public Task<bool> CompleteTask(string id);
    public Task<StateTask?> CreatePlaneBuildTaskAsync(string planeStateId, string taskName);
    Task<ICollection<StateTask>?> GetAllPlaneBuildTasks();
}