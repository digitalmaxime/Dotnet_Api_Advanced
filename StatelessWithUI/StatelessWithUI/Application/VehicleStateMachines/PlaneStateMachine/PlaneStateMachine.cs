using Stateless;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;

namespace StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;

public class PlaneStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public enum PlaneState
    {
        InitialState,
        DesignState,
        BuildState,
        TestingState
    }

    public string VehicleId { get; set; }
    private Enum StateEnum { get; set; }

    public string CurrentStateName => StateEnum.ToString();

    private StateMachine<PlaneState, PlaneAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());

    public PlaneStateMachine(string vehicleId, IServiceScopeFactory serviceScopeFactory)
    {
        VehicleId = vehicleId;
        _serviceScopeFactory = serviceScopeFactory;
        InitializeStateMachine().GetAwaiter().GetResult();
        ConfigureStates();
        // _stateMachine!.Activate();
    }

    ~PlaneStateMachine()
    {
        Console.WriteLine("~PlaneStateMachine xox");
    }

    private async Task InitializeStateMachine()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
        var plane = await planeRepository.GetByIdWithIncludes(VehicleId);
        
        if (plane == null)
        {
            throw new ArgumentException("No plane found with id : {id}");
        }

        StateEnum = Enum.Parse<PlaneState>(plane.GetCurrentStateEnumName());

        _stateMachine = new StateMachine<PlaneState, PlaneAction>(() => (PlaneState)StateEnum, StateMutator);
    }

    async void StateMutator(PlaneState stateEnum)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var stateService = scope.ServiceProvider.GetRequiredService<IStateService>();
        await stateService.CreatePlaneStateAsync(VehicleId, stateEnum);
    }

    private void ConfigureStates()
    {
        _stateMachine.Configure(PlaneState.InitialState)
            .OnActivate(() => { _stateMachine.Fire(PlaneAction.Design); })
            .Permit(PlaneAction.Design, PlaneState.DesignState)
            .OnEntry(async (transition) =>
            {
                EntryAction(transition);
                using var scope = _serviceScopeFactory.CreateScope();
                var stateService = scope.ServiceProvider.GetRequiredService<IStateService>();
                var planeRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
                var plane = await planeRepository.GetById(VehicleId);
                var planeState = plane?.PlaneStates.First(x => x.StateName == PlaneState.InitialState.ToString());
                var updatedState = await stateService.CompleteStateAsync(planeState.Id, PlaneState.InitialState);
            });

        _stateMachine.Configure(PlaneState.DesignState)
            .Permit(PlaneAction.Build, PlaneState.BuildState)
            .OnEntry(async (transition) =>
            {
                EntryAction(transition);
                using var scope = _serviceScopeFactory.CreateScope();
                var stateService = scope.ServiceProvider.GetRequiredService<IStateService>();
                var planeRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
                var plane = await planeRepository.GetByIdWithIncludes(VehicleId);
                var planeState = plane?.PlaneStates.First(x => x.StateName == PlaneState.DesignState.ToString());
                var updatedState = await stateService.CompleteStateAsync(planeState.Id, PlaneState.DesignState);
                
                // TODO: If complete, go to next state
                if (updatedState.IsStateComplete)
                {
                    TakeAction("Build");
                }
                else
                {
                    // TODO: I dunno..
                }
            })
            ;

        _stateMachine.Configure(PlaneState.BuildState)
            .Permit(PlaneAction.Test, PlaneState.TestingState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(PlaneState.TestingState)
            .OnEntryFrom(PlaneAction.Design, EntryAction);
    }

    public void TakeAction(string actionString)
    {
        var success = Enum.TryParse<PlaneAction>(actionString, out var action);
        if (!success)
        {
            throw new ArgumentException($"Invalid action {actionString}");
        }

        switch (action)
        {
            case PlaneAction.Design:
                _stateMachine.Fire(PlaneAction.Design);
                return;

            case PlaneAction.Build:
                _stateMachine.Fire(PlaneAction.Build);
                return;

            case PlaneAction.Test:
                _stateMachine.Fire(PlaneAction.Test);
                return;

            default:
                throw new ArgumentOutOfRangeException(nameof(action), action,
                    $"{nameof(PlaneStateMachine)} does not support {action}");
        }
    }
    
    private void EntryAction(StateMachine<PlaneState, PlaneAction>.Transition transition)
    {
        PrintTransitionState(transition);
    }

    private static void PrintTransitionState(StateMachine<PlaneState, PlaneAction>.Transition transition)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {transition.Source}, " +
            $"State Trigger : {transition.Trigger}, " +
            $"State destination : {transition.Destination}");
    }
}