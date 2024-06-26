namespace StatelessWithUI.Persistence.Domain.PlaneStates;

public class DesignState: StateBase
{
    public override string PlaneEntityId { get; set; }
    public override PlaneEntity PlaneEntity { get; set; }
}