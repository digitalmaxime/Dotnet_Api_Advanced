namespace StatelessWithUI.Application.Features.Plane.Queries;

public class GetPlaneQueryResponseDto
{
    public string? CurrentStateEnumName { get; set; }
    
    public ICollection<PlaneStateNameId> PlaneStateIds { get; set; }
    // public ICollection<string> InitialStateIds { get; set; }
    // public ICollection<string> DesignStateIds { get; set; }
    // public ICollection<string> BuildStateIds { get; set; }
    // public ICollection<string> TestingStateIds { get; set; }
}

public class PlaneStateNameId
{
    public string StateName { get; set; }
    public string StateId { get; set; }
}