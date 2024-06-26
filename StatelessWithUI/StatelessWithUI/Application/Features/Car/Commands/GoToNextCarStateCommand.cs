using MediatR;

namespace StatelessWithUI.Application.Features.Car.Commands;

public record GoToNextCarStateCommand(string Id) : IRequest;