namespace StatelessWithUI.Persistence.Domain;

public class CarEntity: VehicleEntityBase
{
    public int HorsePower { get; set; }
    public override string GetCurrentStateEnumName()
    {
        throw new NotImplementedException();
    }
}