using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public class PlaneEntity: VehicleEntityBase
{
    public ICollection<InitialState> InitialStates { get; }
    public ICollection<DesignState> DesignStates { get; }
    public ICollection<BuildState> BuildStates { get; }
    public ICollection<TestingState> TestingStates { get; }

}