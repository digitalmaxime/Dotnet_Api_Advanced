using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public class GetAllCarQuery: IRequest<IEnumerable<CarEntity>>
{
}