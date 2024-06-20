using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public record CreatePlaneCommand(string Id) : IRequest<VehicleEntityBase?>;