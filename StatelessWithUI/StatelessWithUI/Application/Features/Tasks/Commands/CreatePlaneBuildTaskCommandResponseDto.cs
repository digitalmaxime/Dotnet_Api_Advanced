namespace StatelessWithUI.Controllers;

public record CreatePlaneBuildTaskCommandResponseDto(string TaskId, string TaskName, bool IsComplete, string PlaneBuildStateId);