using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Plane.Commands;

public record CreatePlaneCommand : IRequest<CreatePlaneCommandResponseDto?>;