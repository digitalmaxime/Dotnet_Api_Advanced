using Stateless;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.TestState;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class PlaneStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private enum PlaneAction
    {
        Design,
        Build,
        Test
    }

    public string Id { get; private set; }
    public VehicleStateBase State { get; private set; }
    private VehicleInitialState VehicleInitialState { get; set; }
    private DesignState.DesignState DesignState { get; set; }
    private PlaneStates.BuildState BuildState { get; set; }
    private TestState.TestingState TestingState { get; set; }

    public string CurrentState => State.ToString() ?? "Undefined";
    private readonly StateMachine<VehicleStateBase, PlaneAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());

    public PlaneStateMachine(string id, IServiceScopeFactory serviceScopeFactory)
    {
        Id = id;
        _serviceScopeFactory = serviceScopeFactory;
        _stateMachine = new StateMachine<VehicleStateBase, PlaneAction>(
            () => State,
            (s) =>
            {
                State = s;
                SaveState().GetAwaiter();
            }
        );
        InitializeStateMachine(id).GetAwaiter();
        ConfigureStates();
    }

    ~PlaneStateMachine()
    {
        Console.WriteLine("~PlaneStateMachine xox");
    }

    private async Task InitializeStateMachine(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        var plane = await planeStateRepository.GetById(id);
        Id = id;
        if (plane == null)
        {
            VehicleInitialState = new VehicleInitialState { Id = Guid.NewGuid().ToString() };
            DesignState = new DesignState.DesignState() { Id = Guid.NewGuid().ToString() };
            BuildState = new PlaneStates.BuildState() { Id = Guid.NewGuid().ToString() };
            TestingState = new TestingState() { Id = Guid.NewGuid().ToString() };
            State = VehicleInitialState;
        }
        else
        {
            State = plane.State;
        }

        await SaveState();
    }

    private void ConfigureStates()
    {
        _stateMachine.Configure(VehicleInitialState)
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

    private async void EntryAction(StateMachine<VehicleStateBase, PlaneAction>.Transition transition)
    {
        await SaveState();
        PrintTransitionState(transition);
    }

    private async Task SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        var plane = new PlaneEntity()
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

    private static void PrintTransitionState(StateMachine<VehicleStateBase, PlaneAction>.Transition transition)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {transition.Source}, " +
            $"State Trigger : {transition.Trigger}, " +
            $"State destination : {transition.Destination}");
    }
}