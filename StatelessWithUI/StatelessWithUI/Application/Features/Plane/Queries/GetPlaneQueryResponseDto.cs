namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public class GetPlaneQueryResponseDto
{
    public string CurrentStateEnumName { get; set; }
    
    public ICollection<string> InitialStateIds { get; set; }
    public ICollection<string> DesignStateIds { get; set; }
    public ICollection<string> BuildStateIds { get; set; }
    public ICollection<string> TestingStateIds { get; set; }
    
}