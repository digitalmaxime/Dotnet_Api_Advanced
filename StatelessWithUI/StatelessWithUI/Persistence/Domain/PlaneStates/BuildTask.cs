namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public class BuildTask
{
    public string Id { get; init; }
    public string TaskName { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
    
    public string BuildStateId { get; set; }
    public BuildState BuildState { get; set; }
    
}