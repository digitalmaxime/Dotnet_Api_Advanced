using Stateless;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.Persistence.Domain.PlaneStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneActions;

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
    public Enum StateEnum { get; private set; }

    public string CurrentStateName => StateEnum.ToString();

    private StateMachine<PlaneState, PlaneAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());

    public PlaneStateMachine(string id, IServiceScopeFactory serviceScopeFactory)
    {
        Id = id;

        _serviceScopeFactory = serviceScopeFactory;

        InitializeStateMachine(id).GetAwaiter().GetResult();
        ConfigureStates();
        // _stateMachine!.Activate();
    }

    ~PlaneStateMachine()
    {
        Console.WriteLine("~PlaneStateMachine xox");
    }

    private async Task InitializeStateMachine(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeRepository = scope.ServiceProvider.GetRequiredService<IPlaneRepository>();
        var plane = await planeRepository.GetById(id);
        Id = id;
        if (plane == null)
        {
            throw new ArgumentException("No plane found with id : {id}");
            // StateEnum = PlaneState.InitialState;
            // await CreateState(new InitialState()
            // {
            //     Id = Guid.NewGuid().ToString(), PlaneEntityId = this.Id
            // });
        }

        StateEnum = Enum.Parse<PlaneState>(plane.GetCurrentStateEnumName());

        _stateMachine = new StateMachine<PlaneState, PlaneAction>(
            () => (PlaneState)StateEnum,
            StateMutator
        );

        await SaveState(); // TODO: only if necessary
    }

    async void StateMutator(PlaneState stateEnum)
    {
        var id = Guid.NewGuid().ToString();

        switch (stateEnum)
        {
            case PlaneState.InitialState:
                await CreateState(new InitialState() { Id = id, PlaneEntityId = this.Id });
                break;
            case PlaneState.DesignState:
                await CreateState(new DesignState() { Id = id, PlaneEntityId = this.Id });
                break;
            case PlaneState.BuildState:
                await CreateState(new BuildState() { Id = id, PlaneEntityId = this.Id });
                break;
            case PlaneState.TestingState:
                await CreateState(new TestingState() { Id = id, PlaneEntityId = this.Id });
                break;
            default:
                throw new ArgumentException("not supported state");
        }

        StateEnum = stateEnum;
        await SaveState();
    }

    async Task<string> CreateState<T>(T state) where T : StateBase
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        var res = await planeStateRepository.AddStateAsync(state); // TODO: not good
        return res.Id;
    }

    private void ConfigureStates()
    {
        _stateMachine.Configure(PlaneState.InitialState)
            .OnActivate(() => { _stateMachine.Fire(PlaneAction.Design); })
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
            Id = Id
        };

        await planeStateRepository.SaveAsync(plane);
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