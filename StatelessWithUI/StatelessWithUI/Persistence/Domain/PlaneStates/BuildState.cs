namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public class BuildState : StateBase
{
    public enum BuildTasksEnum
    {
        BuildSoftware,
        BuildWings,
        BuildEngines,
        AssembleWingsEngines,
        IntegrateParts
    }
    
    public bool IsStateComplete => BuildTasks.All(x => x?.IsComplete ?? false);
    public ICollection<BuildTask> BuildTasks { get; set; } = new List<BuildTask>();

    public override string PlaneEntityId { get; set; }
    public override PlaneEntity PlaneEntity { get; set; }
}