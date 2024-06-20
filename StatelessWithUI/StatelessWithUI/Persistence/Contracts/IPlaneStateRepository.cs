using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneStateRepository 
{
    Task<string> AddStateAsync(StateBase state);
    Task<StateBase?> GetStateByVehicleId(string id, string stateName);
}