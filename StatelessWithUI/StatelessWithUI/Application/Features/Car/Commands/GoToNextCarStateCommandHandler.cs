using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.Car.Commands;

public class GoToNextCarStateCommandHandler : IRequestHandler<GoToNextCarStateCommand>
{
    private readonly ICarService _carService;

    public GoToNextCarStateCommandHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task Handle(GoToNextCarStateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}