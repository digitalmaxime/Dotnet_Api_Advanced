namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class Node<T>
{
    public T Value { get; }
    public List<Node<T>> Dependencies { get; }

    public Node(T value)
    {
        Value = value;
        Dependencies = new List<Node<T>>();
    }
}