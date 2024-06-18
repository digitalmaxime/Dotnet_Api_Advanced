using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, bool>
    {
        private readonly ICarService _carService;

        public CreateCarCommandHandler(ICarService carService)
        {
            _carService = carService;
        }

        public Task<bool> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            return _carService.CreateAsync(request.Id);
        }
    }
}