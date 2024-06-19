namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

public class VehicleInitialState: VehicleStateBase
{
    protected override string GetStateName()
    {
        return nameof(VehicleInitialState);
    }
}