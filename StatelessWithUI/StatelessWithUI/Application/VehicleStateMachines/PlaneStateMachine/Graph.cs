using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.VehicleStateMachines.PlaneStateMachine;

public class Graph
{
    private readonly Dictionary<StateTask, Node<StateTask>> nodes;

    public Graph()
    {
        nodes = new Dictionary<StateTask, Node<StateTask>>();
    }
    
    

    public Node<StateTask> AddNode(StateTask value)
    {
        if (nodes.TryGetValue(value, out var addNode))
        {
            return addNode;
        }

        var node = new Node<StateTask>(value);
        nodes.Add(value, node);
        return node;
    }

    public void AddDependency(StateTask from, StateTask to)
    {
        var fromNode = AddNode(from);
        var toNode = AddNode(to);
        fromNode.Dependencies.Add(toNode);
    }

    public ICollection<StateTask>? GetDependencies(StateTask node)
    {
        nodes.TryGetValue(node, out var toto);
        return toto?.Dependencies.Select(x => x.Value).ToList();
    }
    
    private void InitializeGraph(List<StateTask> Tasks)
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

    public List<StateTask> TopologicalSort()
    {
        var visited = new HashSet<StateTask>();
        var result = new List<StateTask>();

        foreach (var node in nodes.Values)
        {
            TopologicalSort(node, visited, result);
        }

        return result;
    }

    private void TopologicalSort(Node<StateTask> node, HashSet<StateTask> visited, List<StateTask> result)
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