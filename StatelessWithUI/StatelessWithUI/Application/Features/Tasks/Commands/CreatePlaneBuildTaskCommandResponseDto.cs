namespace StatelessWithUI.Application.Features.Tasks.Commands;

public record CreatePlaneBuildTaskCommandResponseDto(string TaskId, string TaskName, bool IsComplete, string PlaneBuildStateId);