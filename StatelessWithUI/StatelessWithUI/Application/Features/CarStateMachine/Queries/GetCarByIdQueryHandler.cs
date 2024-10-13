using MediatR;
using StatelessWithUI.Application.Features.CarStateMachine.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;


public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarEntity?>
{
    private readonly ICarService _carService;

    public GetCarByIdQueryHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<CarEntity?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await _carService.GetCarEntity(request.Id);
    }
}