using MediatR;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public record CreatePlaneCommand
    (string Id, int Speed, VehicleStateMachines.PlaneStateMachine.PlaneState State) : IRequest<bool>;