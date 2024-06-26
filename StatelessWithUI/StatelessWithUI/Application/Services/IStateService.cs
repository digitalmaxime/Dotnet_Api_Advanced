using StatelessWithUI.Persistence.Domain.PlaneStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Application.Services;

public interface IStateService
{
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
    
    Task<StateBase?> CreatePlaneStateAsync(string requestPlaneId, PlaneStateMachine.PlaneState planeState);
    Task<BuildState> InitializeTasks(string buildStateId);
}