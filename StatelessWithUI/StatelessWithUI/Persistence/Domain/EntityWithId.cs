namespace StatelessWithUI.Persistence.Domain;

public abstract class EntityWithId
{
    public required string Id { get; init; }
}