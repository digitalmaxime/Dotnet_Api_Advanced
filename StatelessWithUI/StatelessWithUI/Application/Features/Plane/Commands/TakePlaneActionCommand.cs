using MediatR;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public record TakePlaneActionCommand(string Id, string Action): IRequest<bool>;