// namespace StatelessWithUI.Application.Features.CarStateMachine.Commands;
//
// public class CreatecarCommandHandler
// {
//     
// }
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Application.Features.CarStateMachine.Commands;
using StatelessWithUI.Persistence.Domain;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using StatelessWithUI.Persistence.Contracts;

namespace StatelessWithUI.Application.Features.CarStateMachine.Handlers
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, bool>
    {
        // Inject your repository or service that will handle the creation of the CarEntity
        private readonly ICarStateRepository _carService;

        public CreateCarCommandHandler(ICarStateRepository carService)
        {
            _carService = carService;
        }

        public async Task<bool> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new CarEntity
            {
                Id = request.Id,
                Speed = request.Speed,
                State = request.State
            };

            return await _carService.Save(car);
        }
    }
}