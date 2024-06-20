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

    public async Task AddStateAsync(StateBase state)
    {
        await _dbContext.Set<StateBase>().AddAsync(state);
        await _dbContext.SaveChangesAsync();
    }
}
// public class PlaneInitialStateRepository: IPlaneStateRepository
// {
//     private readonly VehicleDbContext _dbContext;
//
//     public PlaneInitialStateRepository(VehicleDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }
//
//     public async Task AddState(StateBase state)
//     {
//         await _dbContext.Set<StateBase>().AddAsync(state);
//         await _dbContext.SaveChangesAsync();
//     }
// }