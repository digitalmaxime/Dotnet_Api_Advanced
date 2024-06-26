namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public abstract class StateBase
{
    public string Id { get; init; } = null!;
    public ICollection<StateTask> StateTask { get; set; } = new List<StateTask>();
    public string StateName => GetType().Name;
    public bool IsStateComplete => StateTask.Any() && StateTask.All(x => x.IsComplete);
    public string PlaneEntityId { get; set; }
    public PlaneEntity PlaneEntity { get; set; }

}