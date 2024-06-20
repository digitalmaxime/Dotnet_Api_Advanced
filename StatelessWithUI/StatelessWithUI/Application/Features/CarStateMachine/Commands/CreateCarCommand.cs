using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands;

public record CreateCarCommand(string Id): IRequest<CarSnapshotEntity?>;