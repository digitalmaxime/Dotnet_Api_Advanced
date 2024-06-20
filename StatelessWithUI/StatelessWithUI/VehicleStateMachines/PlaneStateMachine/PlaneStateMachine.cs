using Stateless;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneActions;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

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

    public string Id { get; private set; }
    public Enum State { get; private set; }
    public string StateId { get; set; }

    public string CurrentStateName => State.ToString();

    private StateMachine<PlaneState, PlaneAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());

    public PlaneStateMachine(string id, IServiceScopeFactory serviceScopeFactory)
    {
        Id = id;
        
        _serviceScopeFactory = serviceScopeFactory;

        InitializeStateMachine(id).GetAwaiter().GetResult();
        ConfigureStates();
    }

    ~PlaneStateMachine()
    {
        Console.WriteLine("~PlaneStateMachine xox");
    }

    private async Task InitializeStateMachine(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        var plane = await planeRepository.GetById(id);
        Id = id;
        if (plane == null)
        {
            State = PlaneState.InitialState;
            var stateId = await planeStateRepository.AddStateAsync(new InitialState() { Id = Guid.NewGuid().ToString() });
            StateId = stateId;
        }
        else
        {
            State = Enum.Parse<PlaneState>(plane.StateEnumName);
            StateId = plane.StateId;
        }

        async void StateMutator(PlaneState s)
        {
            State = s;
            await SaveState();
        }

        _stateMachine = new StateMachine<PlaneState, PlaneAction>(
            () => (PlaneState)State,
            StateMutator
        );

        await SaveState();
    }

    private void ConfigureStates()
    {
        _stateMachine.Configure(PlaneState.InitialState)
            .Permit(PlaneAction.Design, PlaneState.DesignState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(PlaneState.DesignState)
            .Permit(PlaneAction.Build, PlaneState.BuildState)
            .OnEntry(EntryAction)
            ;

        _stateMachine.Configure(PlaneState.BuildState)
            .Permit(PlaneAction.Test, PlaneState.TestingState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(PlaneState.TestingState)
            .OnEntryFrom(PlaneAction.Design, EntryAction);
    }

    private async void EntryAction(StateMachine<PlaneState, PlaneAction>.Transition transition)
    {
        PrintTransitionState(transition);
    }

    private async Task SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
        var plane = new PlaneEntity
        {
            Id = Id, StateEnumName = State.ToString(), StateId = StateId
        };

        await planeStateRepository.SaveAsync(plane);
    }


    public void GoToNextState()
    {
        var nextAvailableAction = _stateMachine.GetPermittedTriggers().OrderDescending().FirstOrDefault();
        _stateMachine.Fire(nextAvailableAction);
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

    private static void PrintTransitionState(StateMachine<PlaneState, PlaneAction>.Transition transition)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {transition.Source}, " +
            $"State Trigger : {transition.Trigger}, " +
            $"State destination : {transition.Destination}");
    }
}