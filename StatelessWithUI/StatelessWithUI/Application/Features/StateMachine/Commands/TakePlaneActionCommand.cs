using MediatR;

namespace StatelessWithUI.Application.Features.Plane.Commands;

public record TakePlaneActionCommand(string Id, string Action): IRequest<bool>;