using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Repositories;

public class PlaneBuildStateRepository: IPlaneStateRepository
{
    private readonly VehicleDbContext _dbContext;

    public PlaneBuildStateRepository(VehicleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> AddStateAsync(StateBase state)
    {
        var entity = await _dbContext.Set<StateBase>().AddAsync(state);
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
}