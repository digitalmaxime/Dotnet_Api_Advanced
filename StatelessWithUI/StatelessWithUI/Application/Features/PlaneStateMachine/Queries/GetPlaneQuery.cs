using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public record GetPlaneQuery(string Id) : IRequest<PlaneVehicleEntity?>;