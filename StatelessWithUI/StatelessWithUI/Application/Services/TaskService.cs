using Microsoft.JSInterop.Infrastructure;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.Features.Tasks.Queries;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public class TaskService : ITaskService
{
    private readonly IBuildTaskRepository _buildTaskRepository;

    public TaskService(IBuildTaskRepository buildTaskRepository)
    {
        _buildTaskRepository = buildTaskRepository;
    }

    public async Task<StateTask?> GetBuildTask(string id)
    {
        return await Task.FromResult<StateTask?>(null);
    }
    
    public async Task<StateTask?> CompleteBuildTask(string id)
    {
        return await Task.FromResult<StateTask?>(null);
    }

    public async Task<StateTask?> CreatePlaneBuildTask(string planeStateId, string taskName)
    {
        var res = await _buildTaskRepository.CreatePlaneBuildTaskAsync(planeStateId, taskName);
        return res;
    }

    public async Task<GetAllBuildPlaneTaskQueryResponseDto?> GetAllPlaneBuildTasksAsync()
    {
        var res = await _buildTaskRepository.GetAllPlaneBuildTasks();
        var dto = new GetAllBuildPlaneTaskQueryResponseDto();
        dto.BuildStateTasks = new Dictionary<string, Asdf>();
        foreach (var x in res)
        {
            if (dto.BuildStateTasks.TryGetValue(x.BuildStateId, out var task))
            {
                task.BuildStateTasks.Add(x);
            }
            else
            {
                dto.BuildStateTasks.Add(x.BuildStateId, new Asdf()
                {
                    StateId = x.BuildStateId,
                    BuildStateTasks = new List<StateTask> { x }
                });
                
            }
        }
        
        return dto;
    }
}