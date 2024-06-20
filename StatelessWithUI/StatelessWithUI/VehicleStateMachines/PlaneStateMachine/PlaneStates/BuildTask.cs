namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class BuildTask
{
    public string Id { get; init; }
    public string TaskName { get; set; } = string.Empty;

    public bool IsComplete { get; set; }
    
    public BuildTask()
    {
        Id = Guid.NewGuid().ToString();
    }
}