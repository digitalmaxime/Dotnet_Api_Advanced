namespace StatelessWithUI.Application.Features.Tasks.Queries;

public record GetPlaneBuildTaskQueryResponseDto()
{
    public string Id { get; set; }
    public bool IsComplete { get; set; }
    public object VehicleId { get; set; }
};