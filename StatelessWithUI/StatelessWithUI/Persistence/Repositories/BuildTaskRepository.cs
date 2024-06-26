using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Repositories;

public class BuildTaskRepository : IBuildTaskRepository
{
    private readonly VehicleDbContext _context;

    public BuildTaskRepository(VehicleDbContext context)
    {
        _context = context;
    }

    public async Task<StateTask?> GetTaskByIdAsync(string id)
    {
        var task = await _context.BuildTask
            // .Include(x => x.BuildState) // TODO:
            .FirstOrDefaultAsync(x => x.Id == id);
        return task;
    }

    public async Task<bool> CompleteTask(string id)
    {
        var task = await _context.BuildTask.FirstOrDefaultAsync(x => x.Id == id);
        if (task == null) return false;
        task.IsComplete = true;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<StateTask?> CreatePlaneBuildTaskAsync(string planeStateId, string taskName)
    {
        var newTask = new StateTask()
        {
            Id = Guid.NewGuid().ToString(),
            TaskName = taskName,
            BuildStateId = planeStateId
        };

        var res = await _context.BuildTask.AddAsync(newTask);
        return await _context.SaveChangesAsync() != 1 ? null : res.Entity;
    }

    public async Task<ICollection<StateTask>?> GetAllPlaneBuildTasks()
    {
        return await _context.BuildTask.ToListAsync();
    }
}