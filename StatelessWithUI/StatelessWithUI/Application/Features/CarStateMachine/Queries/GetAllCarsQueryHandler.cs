using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public class GetAllCarsQueryHandler: IRequestHandler<GetAllCarsQuery, IEnumerable<CarSnapshotEntity>>
{
    private readonly ICarService _carService;

    public GetAllCarsQueryHandler(ICarService carService)
    {
        _carService = carService;
    }
    
    public async Task<IEnumerable<CarSnapshotEntity>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        return await _carService.GetAll();
    }
}