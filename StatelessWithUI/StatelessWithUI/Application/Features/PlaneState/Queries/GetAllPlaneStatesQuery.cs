using MediatR;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public record GetAllPlaneStatesQuery
    (VehicleStateMachines.PlaneStateMachine.PlaneStateMachine.PlaneState PlaneState)
    : IRequest<GetAllPlaneStatesQueryResponseDto?>;