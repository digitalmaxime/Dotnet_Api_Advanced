using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Services;

public class StateService : IStateService
{
    private readonly IPlaneStateRepository _planeStateRepository;
    private readonly ITaskService _taskService;

    public StateService(IPlaneStateRepository planeStateRepository, ITaskService taskService)
    {
        _planeStateRepository = planeStateRepository;
        _taskService = taskService;
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

    public async Task<StateBase?> CreatePlaneStateAsync(string requestPlaneId, PlaneStateMachine.PlaneState planeState)
    {
        StateBase? res;
        StateBase state;
        
        switch (planeState)
        {
            case PlaneStateMachine.PlaneState.InitialState:
                state = new InitialState() { Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId };
                res = await _planeStateRepository.AddStateAsync((InitialState)state);
                break;
            case PlaneStateMachine.PlaneState.DesignState:
                state = new DesignState() { Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId };
                res = await _planeStateRepository.AddStateAsync((DesignState)state);
                break;
            case PlaneStateMachine.PlaneState.BuildState:
                state = new BuildState() { Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId };
                res = await _planeStateRepository.AddStateAsync((BuildState)state);
                break;
            case PlaneStateMachine.PlaneState.TestingState:
                state = new TestingState() { Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId };
                res = await _planeStateRepository.AddStateAsync((TestingState)state);
                break;
            default:
                throw new InvalidOperationException("Invalid state name");
        }

        return res;
    }
    
    public async Task<BuildState?> InitializeBuildStates(string buildStateId)
    {
        // get buildState from db
        var buildState = await _planeStateRepository.GetBuildState(buildStateId);
        
        // if not exists --> return
        if (buildState == null) return null;
        
        // if task exists and has tasks already --> return
        // if (buildState.BuildTasks.Any()) return buildState;
        if (buildState.StateTask.Any()) return buildState;
        
        // if no tasks, init
        var values = Enum.GetValues(typeof(BuildState.BuildTasksEnum));
        foreach (var taskName in values)
        {
            var taskNameStr = taskName.ToString();
            buildState.StateTask.Add(new StateTask
            {
                Id = Guid.NewGuid().ToString(),
                TaskName = taskNameStr,
                BuildStateId = buildState.Id
            });
        }
        
        // save to db
        var res = await _planeStateRepository.UpdateStateAsync(buildState);
        return res;
    }
}