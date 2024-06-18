using MediatR;
using StatelessWithUI.Controllers;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;


public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarEntity?>
{
    private readonly ICarStateRepository _carStateRepository;

    public GetCarByIdQueryHandler(ICarStateRepository carStateRepository)
    {
        _carStateRepository = carStateRepository;
    }

    public async Task<CarEntity?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await _carStateRepository.GetById(request.Id);
    }
}