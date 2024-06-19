namespace StatelessWithUI.VehicleStateMachines.PlaneStateMachine.TestState;

public class TestingState: VehicleStateBase
{
    protected override string GetStateName()
    {
        return nameof(TestingState);
    }
}