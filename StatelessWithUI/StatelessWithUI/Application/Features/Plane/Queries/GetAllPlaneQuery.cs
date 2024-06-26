using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Plane.Queries;

public record GetAllPlaneQuery : IRequest<IEnumerable<PlaneEntity>>;