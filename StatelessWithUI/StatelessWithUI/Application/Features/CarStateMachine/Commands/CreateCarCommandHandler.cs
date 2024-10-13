using MediatR;
using StatelessWithUI.Application.Features.CarStateMachine.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarEntity?>
    {
        private readonly ICarService _carService;

        public CreateCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public Task<CarEntity?> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            return _carService.CreateAsync(request.Id);
        }
    }
}