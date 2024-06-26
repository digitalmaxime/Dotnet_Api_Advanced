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

    public async Task<StateBase?> CompleteStateAsync(string stateId, PlaneStateMachine.PlaneState planeState)
    {
        var result = await _planeStateRepository.GetState(stateId, planeState);
        if (result == null)
            throw new ArgumentException($"{nameof(CompleteStateAsync)} - no StateBase found, Invalid StateId");

        foreach (var s in result.StateTasks)
        {
            await _taskService.CompleteTaskAsync(s.Id);
        }

        return result;
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
                state = new InitialState()
                {
                    Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId,
                    StateTasks = new List<StateTask>()
                    {
                        new StateTask()
                            { Id = Guid.NewGuid().ToString(), TaskName = "Do initial stuff", IsComplete = false }
                    }
                };
                res = await _planeStateRepository.AddStateAsync((InitialState)state);
                break;
            case PlaneStateMachine.PlaneState.DesignState:
                state = new DesignState()
                {
                    Id = Guid.NewGuid().ToString(), PlaneEntityId = requestPlaneId,
                    StateTasks = new List<StateTask>()
                    {
                        new StateTask()
                            { Id = Guid.NewGuid().ToString(), TaskName = "Do design stuff", IsComplete = false }
                    }
                };
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

    public async Task<BuildState?> InitializeBuildStateTasks(string buildStateId)
    {
        // get buildState from db
        var buildState = await _planeStateRepository.GetBuildState(buildStateId);

        // if not exists --> return
        if (buildState == null) return null;

        // if task exists and has tasks already --> return
        if (buildState.StateTasks.Any()) return buildState;

        // if no tasks, init
        var values = Enum.GetValues(typeof(BuildState.StateTasksEnum));
        foreach (var taskName in values)
        {
            var taskNameStr = taskName.ToString();
            buildState.StateTasks.Add(new StateTask
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

    // public async Task<StateBase?> InitializeInitialStateTasks(string stateId, PlaneStateMachine.PlaneState planeState)
    // {
    //     // get buildState from db
    //     var state = await _planeStateRepository.GetState(stateId, planeState);
    //     
    //     // if not exists --> return
    //     if (state == null) return null;
    //     
    //     // if task exists and has tasks already --> return
    //     if (state.StateTasks.Any()) return state;
    //     
    //     // if no tasks, init
    //     var values = Enum.GetValues(typeof(BuildState.StateTasksEnum));
    //     foreach (var taskName in values)
    //     {
    //         var taskNameStr = taskName.ToString();
    //         state.StateTasks.Add(new StateTask
    //         {
    //             Id = Guid.NewGuid().ToString(),
    //             TaskName = taskNameStr,
    //             BuildStateId = state.Id
    //         });
    //     }
    //     
    //     // save to db
    //     var res = await _planeStateRepository.UpdateStateAsync(state);
    //     return res;
    // }
}