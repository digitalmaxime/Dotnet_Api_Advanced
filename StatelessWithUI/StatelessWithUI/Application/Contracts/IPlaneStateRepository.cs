using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.Persistence.Domain.PlaneStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneStateRepository 
{
    Task<T> AddStateAsync<T>(T state) where T : StateBase;
    Task<T> UpdateStateAsync<T>(T state) where T : StateBase;
    
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);

    Task<BuildState?> GetBuildState(string id);

    Task<BuildState> AddBuildStateAsync(BuildState state);
}