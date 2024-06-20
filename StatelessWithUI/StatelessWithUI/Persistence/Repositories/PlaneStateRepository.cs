using Microsoft.EntityFrameworkCore;
using Stateless.Graph;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneStateRepository : IPlaneStateRepository
{
    private readonly VehicleDbContext _dbContext;

    public PlaneStateRepository(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> AddStateAsync<T>(T state) where T : StateBase
    {
        var entity = await _dbContext.Set<T>().AddAsync(state);
        await _dbContext.SaveChangesAsync();
        return entity.Entity.Id;
    }

    public async Task<StateBase?> GetStateByVehicleId(string id, string stateName)
    {
        StateBase? state = stateName switch
        {
            "InitialState" => await _dbContext.Set<InitialState>().FirstOrDefaultAsync(x => x.Id == id),
            "DesignState" => await _dbContext.Set<DesignState>().FirstOrDefaultAsync(x => x.Id == id),
            "BuildState" => await _dbContext.Set<BuildState>().FirstOrDefaultAsync(x => x.Id == id),
            "TestingState" => await _dbContext.Set<TestingState>().FirstOrDefaultAsync(x => x.Id == id),
            _ => throw new InvalidOperationException("Invalid state name")
        };

        return state;
    }

    public async Task<StateBase?> GetState(string id, PlaneStateMachine.PlaneState planeState)
    {
        StateBase? state = planeState switch
        {
            PlaneStateMachine.PlaneState.InitialState => await _dbContext.Set<InitialState>()
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.DesignState => await _dbContext.Set<DesignState>()
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.BuildState => await _dbContext.Set<BuildState>()
                .FirstOrDefaultAsync(x => x.Id == id),
            PlaneStateMachine.PlaneState.TestingState => await _dbContext.Set<TestingState>()
                .FirstOrDefaultAsync(x => x.Id == id),
            _ => throw new InvalidOperationException("Invalid state name")
        };

        return state;
    }
}