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
}