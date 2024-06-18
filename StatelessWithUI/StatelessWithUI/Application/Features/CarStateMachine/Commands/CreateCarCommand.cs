using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StatelessWithUI.Application.Features.CarStateMachine.Commands;

public record CreateCarCommand(string Id, int Speed, VehicleStateMachines.CarStateMachine.CarState State) : IRequest<bool>;