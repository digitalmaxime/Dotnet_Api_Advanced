using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public class PlaneEntity: VehicleEntityBase
{
    public enum PlaneStateNameEnum
    {
        InitialState,
        DesignState,
        BuildState,
        TestingState
    }
    
    public ICollection<StateBase> PlaneStates { get; set; }
    // public ICollection<InitialState> InitialStates { get; }
    // public ICollection<DesignState> DesignStates { get; }
    // public ICollection<BuildState> BuildStates { get; }
    // public ICollection<TestingState> TestingStates { get; }

    public override string GetCurrentStateEnumName()
    {
        return "PATATE"; // TODO:
    }
}