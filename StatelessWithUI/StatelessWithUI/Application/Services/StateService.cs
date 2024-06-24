using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Application.Services;

public class StateService: IStateService
{
    private readonly IPlaneStateRepository _planeStateRepository;

    public StateService(IPlaneStateRepository planeStateRepository)
    {
        _planeStateRepository = planeStateRepository;
    }
    
    public async Task<IEnumerable<StateBase>?> GetAllStates(PlaneStateMachine.PlaneState planeState)
    {
        var result = await _planeStateRepository.GetAllStates(planeState);

        throw new NotImplementedException();
    }

    public async Task<StateBase?> GetState(string id, PlaneStateMachine.PlaneState planeState)
    {
        throw new NotImplementedException();
    }
}