using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Contracts;

public interface IBuildTaskRepository
{
    public Task<BuildTask?> GetTaskByIdAsync(string id);
    public Task<bool> CompleteTask(string id);
    public Task<BuildTask?> CreatePlaneBuildTaskAsync(string planeStateId, string taskName);
    Task<ICollection<BuildTask>?> GetAllPlaneBuildTasks();
}