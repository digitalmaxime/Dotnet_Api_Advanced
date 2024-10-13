using MediatR;
using StatelessWithUI.Application.Features.CarStateMachine.Services;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands;

public class GoToNextCarStateCommandHandler : IRequestHandler<GoToNextCarStateCommand>
{
    private readonly ICarService _carService;

    public GoToNextCarStateCommandHandler(ICarService carService)
    {
        _carService = carService;
    }

    public async Task Handle(GoToNextCarStateCommand request, CancellationToken cancellationToken)
    {
        _carService.GoToNextState(request.Id);
    }
}