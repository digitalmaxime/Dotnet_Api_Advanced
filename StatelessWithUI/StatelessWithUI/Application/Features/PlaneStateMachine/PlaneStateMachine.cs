using Stateless;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines;
using static System.Int32;

namespace StatelessWithUI.Application.Features.PlaneStateMachine;

public class PlaneStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public enum PlaneState
    {
        Stopped,
        Started,
        Running,
        Flying,
    }

    public enum PlaneAction
    {
        Stop,
        Start,
        Accelerate,
        Decelerate,
        Fly,
        Land
    }

    public string EntityId => Plane.Id;
    public PlaneEntity Plane { get; }
    private readonly StateMachine<PlaneState, PlaneAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());
    public string GetCurrentState => Plane.State.ToString();

    private StateMachine<PlaneState, PlaneAction>.TriggerWithParameters<int>? _accelerateWithParam;
    private StateMachine<PlaneState, PlaneAction>.TriggerWithParameters<int>? _decelerateWithParam;

    public PlaneStateMachine(PlaneEntity plane, IServiceScopeFactory serviceScopeFactory)
    {
        Plane = plane;
        _serviceScopeFactory = serviceScopeFactory;
        _stateMachine = new StateMachine<PlaneState, PlaneAction>(
            () => Plane.State,
            (s) =>
            {
                Plane.State = s;
                SaveState().GetAwaiter().GetResult();
            }
        );

        ConfigureStates();
    }

    ~PlaneStateMachine()
    {
        Console.WriteLine("~PlaneStateMachine xox");
    }

    private void ConfigureStates()
    {
        _accelerateWithParam = _stateMachine.SetTriggerParameters<int>(PlaneAction.Accelerate);
        _decelerateWithParam = _stateMachine.SetTriggerParameters<int>(PlaneAction.Decelerate);

        _stateMachine.Configure(PlaneState.Stopped)
            .Permit(PlaneAction.Start, PlaneState.Started)
            .OnEntry((state) =>
            {
                Plane.Speed = 0;
                PrintState(state);
            })
            .OnExit(PrintState);

        _stateMachine.Configure(PlaneState.Started)
            .Permit(PlaneAction.Accelerate, PlaneState.Running)
            .Permit(PlaneAction.Stop, PlaneState.Stopped)
            .OnEntry((state) =>
            {
                Plane.Speed = 0;
                PrintState(state);
            })
            .OnExit(PrintState);
        _stateMachine.Configure(PlaneState.Running)
            .OnEntryFrom(_accelerateWithParam, (speed, _) =>
            {
                Plane.Speed += 35;
                SaveState().GetAwaiter().GetResult();
                Console.WriteLine($"\tSpeed is {Plane.Speed}");
            })
            .PermitIf(PlaneAction.Stop, PlaneState.Stopped, () => Plane.Speed == 0)
            .PermitIf(PlaneAction.Fly, PlaneState.Flying, () => Plane.Speed > 100)
            .InternalTransition(PlaneAction.Accelerate, () =>
            {
                Plane.Speed += 35;
                SaveState().GetAwaiter().GetResult();
                Console.WriteLine($"\tSpeed is {Plane.Speed}");
            })
            .InternalTransitionIf<int>(_decelerateWithParam, _ => Plane.Speed > 0, (speed, _) =>
            {
                Plane.Speed = speed;
                SaveState().GetAwaiter().GetResult();
                Console.WriteLine($"\tSpeed is {Plane.Speed}");
            });

        _stateMachine.Configure(PlaneState.Flying)
            .Permit(PlaneAction.Land, PlaneState.Running);
    }

    private async Task SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var planeStateRepository = scope.ServiceProvider.GetRequiredService<IPlaneStateRepository>();
        await planeStateRepository.Save(Plane);
    }

    public void GoToNextState()
    {
        var nextAvailableAction = _stateMachine.GetPermittedTriggers().OrderDescending().FirstOrDefault();
        _stateMachine.Fire(nextAvailableAction);
    }

    public void TakeAction(string actionString)
    {
        Enum.TryParse<PlaneAction>(actionString, out var action);

        switch (action)
        {
            case PlaneAction.Stop:
                _stateMachine.Fire(PlaneAction.Stop);
                return;

            case PlaneAction.Start:
                _stateMachine.Fire(PlaneAction.Start);
                return;

            case PlaneAction.Accelerate:
                _stateMachine.Fire(_accelerateWithParam, Plane.Speed + 35);
                return;

            case PlaneAction.Decelerate:
                _stateMachine.Fire(_decelerateWithParam, Max(Plane.Speed - 35, 0));
                return;

            case PlaneAction.Fly:
                _stateMachine.Fire(PlaneAction.Fly);
                return;

            case PlaneAction.Land:
                _stateMachine.Fire(PlaneAction.Land);
                return;

            default:
                throw new ArgumentOutOfRangeException(nameof(action), action,
                    $"{nameof(PlaneStateMachine)} does not support {action}");
        }
    }

    private static void PrintState(StateMachine<PlaneState, PlaneAction>.Transition state)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {state.Source}, " +
            $"State Trigger : {state.Trigger}, " +
            $"State destination : {state.Destination}");
    }
}