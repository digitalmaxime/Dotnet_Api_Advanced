using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public interface IStateService
{
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);
    Task<StateBase?> CompleteStateAsync(string stateId, PlaneStateMachine.PlaneState planeState);
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
    
    Task<StateBase?> CreatePlaneStateAsync(string requestPlaneId, PlaneStateMachine.PlaneState planeState);
    Task<BuildState?> InitializeBuildStateTasks(string stateId);
}