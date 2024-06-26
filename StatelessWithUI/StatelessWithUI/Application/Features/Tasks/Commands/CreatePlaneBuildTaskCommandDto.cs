using MediatR;

namespace StatelessWithUI.Controllers;

public record CreatePlaneBuildTaskCommandDto(string planeStateId, string TaskName) : IRequest<CreatePlaneBuildTaskCommandResponseDto?>;