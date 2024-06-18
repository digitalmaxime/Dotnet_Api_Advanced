namespace StatelessWithUI.Persistence.Domain;

public abstract class EntityWithId
{
    public string Id { get; init; } = null!;
}