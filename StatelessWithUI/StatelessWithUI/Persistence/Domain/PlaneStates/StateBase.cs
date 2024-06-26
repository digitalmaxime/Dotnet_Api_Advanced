namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public abstract class StateBase
{
    public string Id { get; init; } = null!;

    public string GetStateName() => GetType().Name;
    public abstract string PlaneEntityId { get; set; }
    public abstract PlaneEntity PlaneEntity { get; set; }

}