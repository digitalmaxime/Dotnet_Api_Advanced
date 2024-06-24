using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public record GetPlaneBuildStateQueryResponseDto
{
    public string StateName { get; init; } = null!;
    public string StateId { get; init; } = null!;
    public ICollection<BuildTaskNode> BuildTaskNodes { get; set; }
};

public record BuildTaskNode
{
    public BuildTask BuildTask { get; set; }
    public ICollection<BuildTask>? Dependencies { get; set; }
};
