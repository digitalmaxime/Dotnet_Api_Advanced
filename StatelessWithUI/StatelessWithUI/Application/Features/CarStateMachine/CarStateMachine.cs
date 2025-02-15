using Stateless;
using Stateless.Graph;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines;
using static System.Int32;

namespace StatelessWithUI.Application.Features.CarStateMachine;

public class CarStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public enum CarState
    {
        Stopped,
        Started,
        Running,
        Drifting,
    }

    public enum CarAction
    {
        Stop,
        Start,
        Accelerate,
        Decelerate,
        Drift
    }

    public string EntityId { get; set; }
    private int CurrentSpeed { get; set; }
    private CarState CurrentState { get; set; }
    public string GetCurrentState => CurrentState.ToString();
    private readonly StateMachine<CarState, CarAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());


    private StateMachine<CarState, CarAction>.TriggerWithParameters<int>? _accelerateWithParam;
    private StateMachine<CarState, CarAction>.TriggerWithParameters<int>? _decelerateWithParam;


    public CarStateMachine(string entityId, IServiceScopeFactory serviceScopeFactory)
    {
        EntityId = entityId;
        _serviceScopeFactory = serviceScopeFactory;

        _stateMachine = new StateMachine<CarState, CarAction>(
            () => CurrentState,
            (s) =>
            {
                CurrentState = s;
                SaveState();
            }
        );

        InitializeStateMachine(entityId).GetAwaiter();
        ConfigureStates();
    }

    ~CarStateMachine()
    {
        Console.WriteLine("~CarStateMachine xox");
    }

    private async Task InitializeStateMachine(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var carStateRepository = scope.ServiceProvider.GetRequiredService<ICarStateRepository>();
        var car = await carStateRepository.GetById(id);
        if (car == null)
        {
            car = new CarEntity()
            {
                Id = id, Speed = 0, State = CarState.Stopped
            };

            await carStateRepository.Save(car);
        }

        CurrentState = car.State;
        CurrentSpeed = car.Speed;
    }

    private void ConfigureStates()
    {
        _accelerateWithParam = _stateMachine.SetTriggerParameters<int>(CarAction.Accelerate);
        _decelerateWithParam = _stateMachine.SetTriggerParameters<int>(CarAction.Decelerate);

        _stateMachine.Configure(CarState.Stopped)
            .Permit(CarAction.Start, CarState.Started)
            .OnEntry((state) =>
            {
                CurrentSpeed = 0;
                PrintState(state);
            })
            .OnExit(PrintState);

        _stateMachine.Configure(CarState.Started)
            .Permit(CarAction.Accelerate, CarState.Running)
            .Permit(CarAction.Stop, CarState.Stopped)
            .OnEntry((state) =>
            {
                CurrentSpeed = 0;
                PrintState(state);
            })
            .OnExit(PrintState);
        _stateMachine.Configure(CarState.Running)
            .OnEntryFrom(_accelerateWithParam, (speed, _) =>
            {
                CurrentSpeed = speed;
                SaveState();
                Console.WriteLine($"\tSpeed is {CurrentSpeed}");
            })
            .PermitIf(CarAction.Stop, CarState.Stopped, () => CurrentSpeed == 0)
            .InternalTransition<int>(_accelerateWithParam, (speed, _) =>
            {
                CurrentSpeed = speed;
                SaveState();
                Console.WriteLine($"\tSpeed is {CurrentSpeed}");
            })
            .InternalTransition(CarAction.Accelerate, () =>
            {
                CurrentSpeed += 35;
                SaveState();
                Console.WriteLine($"\tSpeed is {CurrentSpeed}");
            })
            .InternalTransitionIf<int>(_decelerateWithParam, _ => CurrentSpeed > 0, (speed, _) =>
            {
                CurrentSpeed = speed;
                Console.WriteLine($"\tSpeed is {CurrentSpeed}");
            });
    }

    private void SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var carStateRepository = scope.ServiceProvider.GetRequiredService<ICarStateRepository>();
        var carEntity = new CarEntity()
        {
            Id = EntityId, Speed = CurrentSpeed, State = CurrentState
        };

        carStateRepository.Save(carEntity);
    }

    public void GoToNextState()
    {
        var nextAvailableAction = _stateMachine.GetPermittedTriggers().OrderDescending().FirstOrDefault();
        _stateMachine.Fire(nextAvailableAction);
    }

    public void TakeAction(string actionString)
    {
        Enum.TryParse<CarAction>(actionString, out var carAction);

        switch (carAction)
        {
            case CarAction.Stop:
                _stateMachine.Fire(CarAction.Stop);
                return;

            case CarAction.Start:
                _stateMachine.Fire(CarAction.Start);
                return;

            case CarAction.Accelerate:
                _stateMachine.Fire(_accelerateWithParam, Min(CurrentSpeed + 25, 100));
                return;

            case CarAction.Decelerate:
                _stateMachine.Fire(_decelerateWithParam, Max(CurrentSpeed - 25, 0));
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(carAction), carAction,
                    $"{nameof(CarStateMachine)} does not support {carAction}");
        }
    }

    private static void PrintState(StateMachine<CarState, CarAction>.Transition state)
    {
        Console.WriteLine(
            $"\tOnEntry/OnExit\n\tState Source : {state.Source}, " +
            $"State Trigger : {state.Trigger}, " +
            $"State destination : {state.Destination}");
    }

    public string GetGraph()
    {
        return UmlDotGraph.Format(_stateMachine.GetInfo());
    }
}