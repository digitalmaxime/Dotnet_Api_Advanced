using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Contracts;

public interface IPlaneStateRepository 
{
    Task<T> AddStateAsync<T>(T state) where T : StateBase;
    Task<T> UpdateStateAsync<T>(T state) where T : StateBase;
    
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);

    Task<BuildState?> GetBuildState(string id);

    Task<BuildState> AddBuildStateAsync(BuildState state);
}