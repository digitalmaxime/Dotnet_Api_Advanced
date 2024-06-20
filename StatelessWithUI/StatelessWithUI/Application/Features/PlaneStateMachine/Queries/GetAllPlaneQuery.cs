using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public record GetAllPlaneQuery : IRequest<IEnumerable<PlaneSnapshotEntity>>;