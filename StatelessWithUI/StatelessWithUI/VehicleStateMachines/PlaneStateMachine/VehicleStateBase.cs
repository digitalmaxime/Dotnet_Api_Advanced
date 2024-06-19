namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class VehicleStateBase
{
    public string Id { get; init; } = null!;

    protected virtual string GetStateName() => GetType().Name;
}