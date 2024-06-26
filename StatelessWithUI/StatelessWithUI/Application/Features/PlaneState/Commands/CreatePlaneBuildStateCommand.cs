using MediatR;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

namespace StatelessWithUI.Application.Features.PlaneState.Commands;

public record CreatePlaneBuildStateCommand(string PlaneId, PlaneStateMachine.PlaneState PlaneState) : IRequest<CreatePlaneBuildStateCommandResponseDto?>;