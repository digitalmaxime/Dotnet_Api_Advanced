using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public record GetPlaneGetPermittedTriggersQuery(string Id) : IRequest<IEnumerable<string>>;