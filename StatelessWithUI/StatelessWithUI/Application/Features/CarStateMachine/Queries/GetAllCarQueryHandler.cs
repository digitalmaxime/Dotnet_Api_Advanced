using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public class GetAllCarQueryHandler: IRequestHandler<GetAllCarQuery, IEnumerable<CarEntity>>
{
    public Task<IEnumerable<CarEntity>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<CarEntity>>(new List<CarEntity>()
        {
            new CarEntity()
            {
                Id = "1",
                Speed = 100,
                State = VehicleStateMachines.CarStateMachine.CarState.Running
            }
        });
    }
}