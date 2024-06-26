using StatelessWithUI.Application.Features.Plane.Queries;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.Persistence.Domain.PlaneStates;
using StatelessWithUI.VehicleStateMachineFactory;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Application.Services;

public class PlaneService : IPlaneService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly IPlaneRepository _planeRepository;
    private readonly IStateService _stateService;

    public PlaneService(IVehicleFactory vehicleFactory, IPlaneRepository planeRepository, IStateService stateService)
    {
        _vehicleFactory = vehicleFactory;
        _planeRepository = planeRepository;
        _stateService = stateService;
    }

    public async Task<IEnumerable<PlaneEntity>> GetAll()
    {
        return await _planeRepository.GetAll();
    }

    public async Task<PlaneEntity?> CreateAsync()
    {
        try
        {
            var createdPlane = await _planeRepository.Create(); // TODO:
            var state = await _stateService.CreatePlaneStateAsync(createdPlane.Id,
                PlaneStateMachine.PlaneState.InitialState);
            return createdPlane;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public string? GetPlaneState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.CurrentStateName;
    }

    public async Task<GetPlaneQueryResponseDto?> GetPlaneEntity(string vehicleId, bool includes = false)
    {
        var entity = includes
            ? await _planeRepository.GetByIdWithIncludes(vehicleId)
            : await _planeRepository.GetById(vehicleId);

        if (entity == null) return null;

        return new GetPlaneQueryResponseDto()
        {
            CurrentStateEnumName = entity.GetCurrentStateEnumName(),
            PlaneStateIds = entity.PlaneStates
                .Select(x => new PlaneStateNameId()
                {
                    StateName = x.GetStateName(), StateId = x.Id
                }).ToList()
            // InitialStateIds = entity.InitialStates
            //     .OrderByDescending(x => x.Id)
            //     .Select(x => x.Id).ToList(),
            // DesignStateIds = entity.DesignStates
            //     .OrderByDescending(x => x.Id)
            //     .Select(x => x.Id).ToList(),
            // BuildStateIds = entity.BuildStates
            //     .OrderByDescending(x => x.Id)
            //     .Select(x => x.Id).ToList(),
            // TestingStateIds = entity.TestingStates
            //     .OrderByDescending(x => x.Id)
            //     .Select(x => x.Id).ToList()
        };
    }

    public async Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId)
    {
        if (await _planeRepository.GetById(vehicleId) == null) return null;

        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.GetPermittedTriggers;
    }

    public async Task<bool> TakeActionAsync(string vehicleId, string action)
    {
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);

        if (stateMachine == null) return false;

        try
        {
            stateMachine.TakeAction(action);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<PlaneEntity> InitializeStates(string planeEnityId)
    {
        // get buildState from db
        var planeEntity = await _planeRepository.GetById(planeEnityId);

        // if not exists --> return
        if (planeEntity == null) return null;

        // if task exists and has tasks already --> return
        if (planeEntity.PlaneStates.Any()) return planeEntity;

        // if no tasks, init
        planeEntity.PlaneStates.Add(new InitialState()
        {
            Id = Guid.NewGuid().ToString(),
            PlaneEntityId = planeEntity.Id
        });
        
        // save to db
        var res = await _planeRepository.UpdateStateAsync(planeEntity);
        return res;
    }
}