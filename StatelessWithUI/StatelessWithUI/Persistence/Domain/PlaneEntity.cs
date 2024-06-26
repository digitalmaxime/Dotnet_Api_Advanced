using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Domain;

public class PlaneEntity: VehicleEntityBase
{
    public enum PlaneStateNameEnum
    {
        InitialState,
        DesignState,
        BuildState,
        TestingState,
        UndefinedState,
    }

    public ICollection<StateBase> PlaneStates { get; set; } = new List<StateBase>();

    public override string? GetCurrentStateEnumName()
    {
        var planeStateEnumerable = Enum.GetValues(typeof(PlaneStateNameEnum));
        foreach(var planeState in planeStateEnumerable)
        {
            var state = PlaneStates.FirstOrDefault(x => x.StateName == planeState.ToString());
            if (state != null && !state.IsStateComplete) return planeState.ToString();
        }

        return PlaneStateNameEnum.UndefinedState.ToString();
    }
}