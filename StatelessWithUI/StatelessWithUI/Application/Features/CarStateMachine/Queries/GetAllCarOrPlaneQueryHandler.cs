using MediatR;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public class GetAllCarOrPlaneQueryHandler: IRequestHandler<GetAllQuery, IEnumerable<EntityWithId>>
{
    private readonly ICarStateRepository _carStateRepository;
    private readonly IPlaneStateRepository _planeStateRepository;

    public GetAllCarOrPlaneQueryHandler(ICarStateRepository carStateRepository, IPlaneStateRepository planeStateRepository)
    {
        _carStateRepository = carStateRepository;
        _planeStateRepository = planeStateRepository;
    }
    public async Task<IEnumerable<EntityWithId>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        if (request.VehicleType == VehicleType.Car)
        {
        return await _carStateRepository.GetAll();
            
        }

        return await _planeStateRepository.GetAll();
    }
}