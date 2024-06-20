using Stateless;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneActions;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class PlaneStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public string Id { get; private set; }
    public StateBase State { get; private set; }
    private InitialState InitialState { get; set; }
    private DesignState DesignState { get; set; }
    private BuildState BuildState { get; set; }
    private TestingState TestingState { get; set; }

    public string CurrentState => State.ToString() ?? "Undefined";
    private StateMachine<StateBase, PlaneAction> _stateMachine;
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
            InitialState = new InitialState { Id = Guid.NewGuid().ToString() };
            DesignState = new DesignState { Id = Guid.NewGuid().ToString() };
            BuildState = new BuildState { Id = Guid.NewGuid().ToString() };
            TestingState = new TestingState { Id = Guid.NewGuid().ToString() };
            // await planeStateRepository.AddStateAsync(InitialState);
            await planeStateRepository.AddStateAsync(DesignState);
            
            State = InitialState;
        }
        else
        {
            State = plane.State;
        }
        async void StateMutator(StateBase s)
        {
            State = s;
            await SaveState();
        }

        _stateMachine = new StateMachine<StateBase, PlaneAction>(
            () => State ?? throw new InvalidOperationException("Plane state is null"),
            StateMutator
        );
        
        await SaveState();
    }

    private void ConfigureStates()
    {
        _stateMachine.Configure(InitialState)
            .Permit(PlaneAction.Design, DesignState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(DesignState)
            .Permit(PlaneAction.Build, BuildState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(BuildState)
            .Permit(PlaneAction.Test, TestingState)
            .OnEntry(EntryAction);

        _stateMachine.Configure(TestingState)
            .OnEntryFrom(PlaneAction.Design, EntryAction);
    }

    private async void EntryAction(StateMachine<StateBase, PlaneAction>.Transition transition)
    {
        await SaveState();
        PrintTransitionState(transition);
    }

    private async Task SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
        var plane = new PlaneVehicleEntity
        {
            Id = Id, State = State, StateId = State.Id
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

    private static void PrintTransitionState(StateMachine<StateBase, PlaneAction>.Transition transition)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {transition.Source}, " +
            $"State Trigger : {transition.Trigger}, " +
            $"State destination : {transition.Destination}");
    }
}