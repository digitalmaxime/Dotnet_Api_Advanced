namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public class BuildState : StateBase
{
    public enum StateTasksEnum
    {
        BuildSoftware,
        BuildWings,
        BuildEngines,
        AssembleWingsEngines,
        IntegrateParts
    }
}