using MediatR;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public record GetPlaneBuildStateQuery(string Id) : IRequest<GetPlaneBuildStateQueryResponseDto?>;