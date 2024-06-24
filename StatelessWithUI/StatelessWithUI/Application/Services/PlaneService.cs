using StatelessWithUI.Application.Features.PlaneStateMachine.Queries;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachineFactory;

namespace StatelessWithUI.Application.Services;

public class PlaneService : IPlaneService
{
    private readonly IVehicleFactory _vehicleFactory;
    private readonly IPlaneRepository _planeRepository;

    public PlaneService(IVehicleFactory vehicleFactory, IPlaneRepository planeRepository)
    {
        _vehicleFactory = vehicleFactory;
        _planeRepository = planeRepository;
    }

    public async Task<IEnumerable<PlaneEntity>> GetAll()
    {
        return await _planeRepository.GetAll();
    }

    public async Task<VehicleEntityBase?> CreateAsync(string vehicleId)
    {
        try
        {
            var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
            return new PlaneEntity()
            {
                Id = stateMachine.Id,
                CurrentStateEnumName = stateMachine.StateEnum.ToString(),
                // StateId = stateMachine.StateId
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public string GetPlaneState(string vehicleId)
    {
        var stateMachine = _vehicleFactory.GetVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine.CurrentStateName;
    }

    public async Task<GetPlaneQueryResponseDto?> GetPlaneEntity(string vehicleId, bool includes = false)
    {
        var entity = includes
            ? await _planeRepository.GetByIdWithIncludes(vehicleId)
            : await _planeRepository.GetById(vehicleId);

        if (entity == null) return null;

        return new GetPlaneQueryResponseDto()
        {
            CurrentStateEnumName = entity.CurrentStateEnumName,
            InitialStateIds = entity.InitialStates
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id).ToList(),
            DesignStateIds = entity.DesignStates
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id).ToList(),
            BuildStateIds = entity.BuildStates
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id).ToList(),
            TestingStateIds = entity.TestingStates
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id).ToList()
        };
    }

    public async Task<IEnumerable<string>?> GetPermittedTriggers(string vehicleId)
    {
        if (await _planeRepository.GetById(vehicleId) == null) return null;
        
        var stateMachine = _vehicleFactory.GetOrAddVehicleStateMachine(VehicleType.Plane, vehicleId);
        return stateMachine?.GetPermittedTriggers;
    }

    public async Task<bool> TakeAction(string vehicleId, string action)
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
}