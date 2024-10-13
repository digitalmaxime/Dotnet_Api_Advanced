using MediatR;
using StatelessWithUI.Application.Features.CarStateMachine.Services;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public class GetAllCarsQueryHandler: IRequestHandler<GetAllCarsQuery, IEnumerable<CarEntity>>
{
    private readonly ICarService _carService;

    public GetAllCarsQueryHandler(ICarService carService)
    {
        _carService = carService;
    }
    
    public async Task<IEnumerable<CarEntity>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        return await _carService.GetAll();
    }
}