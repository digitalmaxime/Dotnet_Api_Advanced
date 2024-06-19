namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.BuildState;

public class BuildTask
{
    public string Id { get; set; }
    public string TaskName { get; set; }
    
    public bool IsComplete { get; set; }
    
    public BuildTask()
    {
        
    }
}