using Stateless;
using Stateless.Graph;
using StatelessWithUI.Application.Contracts;
using StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine.PlaneActions;
using StatelessWithUI.Persistence.Domain;
using static System.Int32;

namespace StatelessWithUI.Application.VehicleStateMachines.CarStateMachine;

public class CarStateMachine : IVehicleStateMachine
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public enum CarState
    {
        InitialState,
        Designed,
        MainFrameBuilt,
        EngineBuilt,
        Assembled,
        Painted
    }
    
    public enum CarAction
    {
        Design,
        BuildMainFrame,
        BuildEngine,
        Assemble,
        IncreaseHorsePower,
        Paint
    }

    public string VehicleId { get; set; }
    public string StateId { get; set; }
    private int HorsePower { get; set; }
    private bool _isEnginBuilt { get; set; }
    private bool _isMainFrameBuilt { get; set; }
    public Enum StateEnum { get; private set; }
    public string CurrentStateName => StateEnum.ToString();
    public void TakeAction(PlaneAction actionString)
    {
        throw new NotImplementedException();
    }

    private readonly StateMachine<CarState, CarAction> _stateMachine;
    public IEnumerable<string> GetPermittedTriggers => _stateMachine.GetPermittedTriggers().Select(x => x.ToString());
    
    private StateMachine<CarState, CarAction>.TriggerWithParameters<int>? _addHorsePower;
    private StateMachine<CarState, CarAction>.TriggerWithParameters<string>? _paintWithColor;

    public CarStateMachine(string id, IServiceScopeFactory serviceScopeFactory)
    {
        VehicleId = id;
        _serviceScopeFactory = serviceScopeFactory;

        _stateMachine = new StateMachine<CarState, CarAction>(
            () => (CarState)StateEnum,
            (s) =>
            {
                StateEnum = s;
                SaveState();
            }
        );

        InitializeStateMachine(id).GetAwaiter();
        ConfigureStates();
    }

    ~CarStateMachine()
    {
        Console.WriteLine("~CarStateMachine xox");
    }

    private async Task InitializeStateMachine(string id)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var carStateRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();
        var car = await carStateRepository.GetById(id);
        if (car == null)
        {
            car = new CarEntity()
            {
                Id = id, HorsePower = 0
            };

            await carStateRepository.SaveAsync(car);
        }

        StateEnum = Enum.Parse<CarState>(car.GetCurrentStateEnumName());
        HorsePower = car.HorsePower;
    }

    private void ConfigureStates()
    {
        _addHorsePower = _stateMachine.SetTriggerParameters<int>(CarAction.Assemble);
        _paintWithColor = _stateMachine.SetTriggerParameters<string>(CarAction.Paint);

        _stateMachine.Configure(CarState.InitialState)
            .OnActivate(() => Console.WriteLine("On Active <--"))
            .Permit(CarAction.Design, CarState.Designed)
            .OnEntry((state) => { PrintState(state); })
            .OnExit(PrintState);

        _stateMachine.Configure(CarState.InitialState)
            .Permit(CarAction.BuildEngine, CarState.EngineBuilt)
            .Permit(CarAction.BuildMainFrame, CarState.MainFrameBuilt)
            .PermitIf(CarAction.Assemble, CarState.Assembled, () => _isEnginBuilt && _isMainFrameBuilt)
            .OnEntry((state) => { PrintState(state); })
            .OnExit(PrintState);
        
        _stateMachine.Configure(CarState.EngineBuilt)
            .SubstateOf(CarState.Designed)
            .PermitIf(CarAction.Assemble, CarState.Assembled, () => _isEnginBuilt && _isMainFrameBuilt)
            .OnEntryFrom(_addHorsePower, (horsePower, _) =>
            {
                HorsePower = horsePower;
                _isEnginBuilt = true;
                SaveState();
                Console.WriteLine($"Entered EngineBuilt State");
            })
            .InternalTransition<int>(_addHorsePower, (horsePower, _) =>
            {
                HorsePower = horsePower;
                SaveState();
                Console.WriteLine($"\tadded {horsePower} horsePower : {HorsePower}");
            })
            .InternalTransition(CarAction.IncreaseHorsePower, () =>
            {
                HorsePower += 35;
                SaveState();
                Console.WriteLine($"\tIncreased horse power, now at {HorsePower}");
            });

        _stateMachine.Configure(CarState.MainFrameBuilt)
            .SubstateOf(CarState.Designed)
            .PermitIf(CarAction.Assemble, CarState.Assembled, () => _isEnginBuilt && _isMainFrameBuilt)
            .OnEntryFrom(CarAction.Design, () =>
            {
                _isMainFrameBuilt = true;
                SaveState();
                Console.WriteLine($"Entered MainFrameBuilt State");
            });

        _stateMachine.Configure(CarState.Assembled)
            .OnEntryFrom(CarAction.Design, () =>
            {
                SaveState();
                Console.WriteLine($"What the fuck!, Entered Assembled State from Design**");
            });
    }

    private void SaveState()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var carStateRepository = scope.ServiceProvider.GetRequiredService<ICarRepository>();
        var carEntity = new CarEntity()
        {
            Id = VehicleId, HorsePower = HorsePower
        };

        carStateRepository.SaveAsync(carEntity);
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
            case CarAction.Design:
                _stateMachine.Fire(CarAction.Design);
                return;
            
            case CarAction.BuildEngine:
                _stateMachine.Fire(_addHorsePower, Min(HorsePower + 25, 100));
                return;
            
            case CarAction.BuildMainFrame:
                _stateMachine.Fire(CarAction.BuildMainFrame);
                return;

            case CarAction.Assemble:
                _stateMachine.Fire(CarAction.Assemble);
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