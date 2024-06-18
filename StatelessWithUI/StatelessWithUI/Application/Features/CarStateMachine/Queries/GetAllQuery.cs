using MediatR;
using StatelessWithUI.Persistence.Constants;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public record GetAllQuery(VehicleType VehicleType): IRequest<IEnumerable<EntityWithId>>;