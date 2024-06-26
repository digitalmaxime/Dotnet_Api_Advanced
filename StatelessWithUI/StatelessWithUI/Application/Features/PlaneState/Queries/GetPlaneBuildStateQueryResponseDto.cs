using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public record GetPlaneBuildStateQueryResponseDto
{
    public string StateName { get; init; } = null!;
    public string StateId { get; init; } = null!;
    public ICollection<BuildTaskNode> BuildTaskNodes { get; set; }
};

public record BuildTaskNode
{
    public StateTask StateTask { get; set; }
    public ICollection<StateTask>? Dependencies { get; set; }
};
