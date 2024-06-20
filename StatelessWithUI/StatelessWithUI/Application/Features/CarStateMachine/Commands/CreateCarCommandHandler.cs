using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CarVehicleEntity?>
    {
        private readonly ICarService _carService;

        public CreateCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public Task<CarVehicleEntity?> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            return _carService.CreateAsync(request.Id);
        }
    }
}