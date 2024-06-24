using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneStateRepository 
{
    Task<string> AddStateAsync<T>(T state) where T : StateBase;
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);

    Task<BuildState?> GetBuildState(string id);

}