using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class Graph2<T> where T : BuildTask
{
    private readonly Dictionary<T, Node<T>> nodes;

    public Graph2()
    {
        nodes = new Dictionary<T, Node<T>>();
    }
    
    

    public Node<T> AddNode(T value)
    {
        if (nodes.TryGetValue(value, out var addNode))
        {
            return addNode;
        }

        var node = new Node<T>(value);
        nodes.Add(value, node);
        return node;
    }

    public void AddDependency(T from, T to)
    {
        var fromNode = AddNode(from);
        var toNode = AddNode(to);
        fromNode.Dependencies.Add(toNode);
    }

    public ICollection<T>? GetDependencies(T node)
    {
        nodes.TryGetValue(node, out var toto);
        return toto?.Dependencies.Select(x => x.Value).ToList();
    }

    public List<T> TopologicalSort()
    {
        var visited = new HashSet<T>();
        var result = new List<T>();

        foreach (var node in nodes.Values)
        {
            TopologicalSort(node, visited, result);
        }

        return result;
    }

    private void TopologicalSort(Node<T> node, HashSet<T> visited, List<T> result)
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