using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class Graph
{
    private readonly Dictionary<BuildTask, Node<BuildTask>> nodes;

    public Graph()
    {
        nodes = new Dictionary<BuildTask, Node<BuildTask>>();
    }
    
    

    public Node<BuildTask> AddNode(BuildTask value)
    {
        if (nodes.TryGetValue(value, out var addNode))
        {
            return addNode;
        }

        var node = new Node<BuildTask>(value);
        nodes.Add(value, node);
        return node;
    }

    public void AddDependency(BuildTask from, BuildTask to)
    {
        var fromNode = AddNode(from);
        var toNode = AddNode(to);
        fromNode.Dependencies.Add(toNode);
    }

    public ICollection<BuildTask>? GetDependencies(BuildTask node)
    {
        nodes.TryGetValue(node, out var toto);
        return toto?.Dependencies.Select(x => x.Value).ToList();
    }
    
    private void InitializeGraph(List<BuildTask> Tasks)
    {
        AddNode(Tasks.First(x => x.TaskName == "GetMaterials"));
        AddDependency(Tasks.First(x => x.TaskName == "BuildSoftware"),
            Tasks.First(x => x.TaskName == "GetMaterials"));
        AddDependency(Tasks.First(x => x.TaskName == "BuildWings"),
            Tasks.First(x => x.TaskName == "GetMaterials"));
        AddDependency(Tasks.First(x => x.TaskName == "BuildEngines"),
            Tasks.First(x => x.TaskName == "GetMaterials"));
        AddDependency(Tasks.First(x => x.TaskName == "AssembleWingsEngines"),
            Tasks.First(x => x.TaskName == "BuildEngines"));
        AddDependency(Tasks.First(x => x.TaskName == "AssembleWingsEngines"),
            Tasks.First(x => x.TaskName == "BuildWings"));
        AddDependency(Tasks.First(x => x.TaskName == "IntegrateParts"),
            Tasks.First(x => x.TaskName == "AssembleWingsEngines"));
        AddDependency(Tasks.First(x => x.TaskName == "IntegrateParts"),
            Tasks.First(x => x.TaskName == "BuildSoftware"));
    }

    public List<BuildTask> TopologicalSort()
    {
        var visited = new HashSet<BuildTask>();
        var result = new List<BuildTask>();

        foreach (var node in nodes.Values)
        {
            TopologicalSort(node, visited, result);
        }

        return result;
    }

    private void TopologicalSort(Node<BuildTask> node, HashSet<BuildTask> visited, List<BuildTask> result)
    {
        if (visited.Contains(node.Value))
        {
            return;
        }

        visited.Add(node.Value);

        foreach (var dependency in node.Dependencies)
        {
            TopologicalSort(dependency, visited, result);
        }

        result.Add(node.Value);
    }
}