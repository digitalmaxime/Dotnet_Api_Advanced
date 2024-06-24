using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Application.Services;

public interface IStateService
{
    Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState);
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);


}