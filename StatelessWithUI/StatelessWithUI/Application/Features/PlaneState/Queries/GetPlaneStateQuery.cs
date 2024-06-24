using MediatR;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public record GetPlaneStateQuery(string Id,  VehicleStateMachines.PlaneStateMachine.PlaneStateMachine.PlaneState PlaneState)
    : IRequest<GetPlaneStateQueryResponseDto?>;