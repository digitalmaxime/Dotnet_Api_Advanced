using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;


public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarVehicleEntity?>
{
    private readonly ICarService _carService;

    public GetCarByIdQueryHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<CarVehicleEntity?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await _carService.GetCarEntity(request.Id);
    }
}