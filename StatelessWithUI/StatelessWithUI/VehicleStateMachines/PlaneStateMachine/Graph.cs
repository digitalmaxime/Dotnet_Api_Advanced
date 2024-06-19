namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class Graph<T> where T : class
{
    private readonly Dictionary<T, Node<T>> nodes;

    public Graph()
    {
        nodes = new Dictionary<T, Node<T>>();
    }

    public Node<T> AddNode(T value)
    {
        if (nodes.ContainsKey(value))
        {
            return nodes[value];
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