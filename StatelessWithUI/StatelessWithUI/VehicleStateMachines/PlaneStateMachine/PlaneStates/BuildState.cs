using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.BuildState;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class BuildState: VehicleStateBase
{
    public bool IsStateComplete => IntegrateParts.IsComplete;
    public BuildTask GetMaterials { get; set; }
    public BuildTask BuildSoftware { get; set; }
    public BuildTask BuildWings { get; set; }
    public BuildTask BuildEngines { get; set; }
    public BuildTask AssembleWingsEngines { get; set; }
    public BuildTask IntegrateParts { get; set; }
    // public Graph<BuildTask> Graph { get; init; }

    public BuildState()
    {
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        // Graph.AddNode(GetMaterials);
        // Graph.AddDependency(BuildSoftware, GetMaterials);
        // Graph.AddDependency(BuildWings, GetMaterials);
        // Graph.AddDependency(BuildEngines, GetMaterials);
        // Graph.AddDependency(AssembleWingsEngines, BuildEngines);
        // Graph.AddDependency(AssembleWingsEngines, BuildWings);
        // Graph.AddDependency(IntegrateParts, AssembleWingsEngines);
        // Graph.AddDependency(IntegrateParts, BuildSoftware);
    }

    protected override string GetStateName()
    {
        return nameof(BuildTask);
    }
}