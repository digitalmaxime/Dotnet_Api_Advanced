using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Queries;

public record GetCarByIdQuery(string Id) : IRequest<CarSnapshotEntity?>;