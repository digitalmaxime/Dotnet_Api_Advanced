using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

public class BuildState : StateBase
{
    public bool IsStateComplete => IntegrateParts.IsComplete;

    public BuildTask GetMaterials { get; set; }
    public BuildTask BuildSoftware { get; set; }
    public BuildTask BuildWings { get; set; }
    public BuildTask BuildEngines { get; set; }
    public BuildTask AssembleWingsEngines { get; set; }
    public BuildTask IntegrateParts { get; set; }
    public Graph<BuildTask> Graph { get; init; }

    public BuildState()
    {
        Graph = new Graph<BuildTask>();
        InitializeTasks();
        InitializeGraph();
    }

    private void InitializeTasks()
    {
        GetMaterials = new BuildTask { TaskName = "Get Materials" };
        BuildSoftware = new BuildTask { TaskName = "Build Software" };
        BuildWings = new BuildTask { TaskName = "Build Wings" };
        BuildEngines = new BuildTask { TaskName = "Build Engines" };
        AssembleWingsEngines = new BuildTask { TaskName = "Assemble Wings and Engines" };
        IntegrateParts = new BuildTask { TaskName = "Integrate Parts" };
    }

    private void InitializeGraph()
    {
        Graph.AddNode(GetMaterials);
        Graph.AddDependency(BuildSoftware, GetMaterials);
        Graph.AddDependency(BuildWings, GetMaterials);
        Graph.AddDependency(BuildEngines, GetMaterials);
        Graph.AddDependency(AssembleWingsEngines, BuildEngines);
        Graph.AddDependency(AssembleWingsEngines, BuildWings);
        Graph.AddDependency(IntegrateParts, AssembleWingsEngines);
        Graph.AddDependency(IntegrateParts, BuildSoftware);
    }
}