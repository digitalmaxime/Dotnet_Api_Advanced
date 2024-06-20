using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Contracts;

public interface IPlaneStateRepository 
{
    Task AddStateAsync(StateBase state);
}