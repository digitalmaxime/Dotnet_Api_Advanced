using MediatR;

namespace StatelessWithUI.Application.Features.Tasks.Commands;

public record CreatePlaneBuildTaskCommandDto(string planeStateId, string TaskName) : IRequest<CreatePlaneBuildTaskCommandResponseDto?>;