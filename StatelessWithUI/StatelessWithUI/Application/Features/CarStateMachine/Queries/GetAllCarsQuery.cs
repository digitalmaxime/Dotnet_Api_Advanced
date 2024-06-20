using MediatR;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public record GetAllCarsQuery: IRequest<IEnumerable<CarVehicleEntity>>;