using MediatR;

namespace StatelessWithUI.Application.Features.Plane.Queries;

public record GetPlaneGetPermittedTriggersQuery(string Id) : IRequest<IEnumerable<string>?>;