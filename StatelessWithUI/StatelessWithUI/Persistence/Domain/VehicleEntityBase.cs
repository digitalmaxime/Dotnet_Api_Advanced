namespace StatelessWithUI.Persistence.Domain;

public abstract class VehicleEntityBase
{
    public string Id { get; init; } = null!;
    public abstract string? GetCurrentStateEnumName();
}