using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Persistence.Domain.CarStates;

public class CarEngineBuiltState: StateBase
{
    public override string PlaneEntityId { get; set; }
    public override PlaneEntity PlaneEntity { get; set; }
}