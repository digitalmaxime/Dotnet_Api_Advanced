using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneStateRepository 
{
    Task<string> AddStateAsync<T>(T state) where T : StateBase;
    Task<StateBase?> GetStateByVehicleId(string id, string stateName);
    Task<StateBase?> GetState(string id,  PlaneStateMachine.PlaneState planeState);
}