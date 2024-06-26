using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneStateRepository : IPlaneStateRepository
{
    private readonly VehicleDbContext _dbContext;

    public PlaneStateRepository(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddStateAsync<T>(T state) where T : StateBase
    {
        var entity = await _dbContext.Set<T>().AddAsync(state);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<T> UpdateStateAsync<T>(T state) where T : StateBase
    {
        var res = _dbContext.Set<T>().Update(state);
        var nb = await _dbContext.SaveChangesAsync();
        
        if (nb < 1) throw new DbUpdateException($"Failed to update state stateid : {state.Id}");
        
        return res.Entity;
    }

    public async Task<BuildState> AddBuildStateAsync(BuildState state)
    {
        var entity = await _dbContext.BuildState.AddAsync(state);
        await _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<StateBase?> GetState(string id, PlaneStateMachine.PlaneState planeState)
    {
        StateBase? state = planeState switch
        {
            PlaneStateMachine.PlaneState.InitialState => await _dbContext.Set<InitialState>()
                .Include(x => x.StateTasks)
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.DesignState => await _dbContext.Set<DesignState>()
                .Include(x => x.StateTasks)
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.BuildState => await _dbContext.Set<BuildState>()
                .Include(x => x.StateTasks)
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.TestingState => await _dbContext.Set<TestingState>()
                .Include(x => x.StateTasks)
                .FirstOrDefaultAsync(x => x.Id == id),
            _ => throw new InvalidOperationException("Invalid state name")
        };

        return state;
    }
    
    public async Task<BuildState?> GetBuildState(string id)
    {
        var buildState = await _dbContext.Set<BuildState>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return buildState;
    }

    public async Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState)
    {
        IEnumerable<StateBase> states = planeState switch
        {
            PlaneStateMachine.PlaneState.InitialState => await _dbContext.Set<InitialState>()
                .Include(x => x.PlaneEntity) // TODO: Not working..
                .ToListAsync(),
            PlaneStateMachine.PlaneState.DesignState => await _dbContext.Set<DesignState>().ToListAsync(),
            PlaneStateMachine.PlaneState.BuildState => await _dbContext.Set<BuildState>().ToListAsync(),
            PlaneStateMachine.PlaneState.TestingState => await _dbContext.Set<TestingState>().ToListAsync(),
            _ => throw new InvalidOperationException("Invalid state name")
        };

        return states;    }
}