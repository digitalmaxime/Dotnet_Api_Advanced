using MediatR;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands;

public record GoToNextCarStateCommand(string Id) : IRequest;