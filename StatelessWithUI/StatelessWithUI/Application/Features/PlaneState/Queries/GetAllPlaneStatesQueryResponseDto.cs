namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public class GetAllPlaneStatesQueryResponseDto
{
    public string StateName { get; set; }
    public IEnumerable<StatePlanePair> StatePlanePairs { get; set; }
}

public record StatePlanePair (string StateId, string PlaneId);