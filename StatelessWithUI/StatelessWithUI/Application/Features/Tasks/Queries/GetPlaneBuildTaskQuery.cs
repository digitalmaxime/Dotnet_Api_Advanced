using MediatR;

namespace StatelessWithUI.Application.Features.Tasks.Queries;

public record GetPlaneBuildTaskQuery(string Id) : IRequest<GetPlaneBuildTaskQueryResponseDto?>;