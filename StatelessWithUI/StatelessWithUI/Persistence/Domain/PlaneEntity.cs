using StatelessWithUI.Application.Features.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines;

namespace StatelessWithUI.Persistence.Domain;

public class PlaneEntity: EntityWithId
{
    public PlaneStateMachine.PlaneState State { get; set; }
    public int Speed { get; set; }
    public int Altitude { get; set; }

}