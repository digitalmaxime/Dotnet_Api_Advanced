using MediatR;

namespace StatelessWithUI.Application.Features.Plane.Queries;

public record GetPlaneQuery(string Id, bool includes) : IRequest<GetPlaneQueryResponseDto?>;