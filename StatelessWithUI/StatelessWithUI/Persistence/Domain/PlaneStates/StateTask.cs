namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public class StateTask
{
    public string Id { get; init; }
    public string TaskName { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
    
    public string BuildStateId { get; set; }
    public StateBase BuildState { get; set; }
    
}