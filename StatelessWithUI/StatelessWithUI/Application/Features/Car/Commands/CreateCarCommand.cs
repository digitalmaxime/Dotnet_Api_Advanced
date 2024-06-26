using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Car.Commands;

public record CreateCarCommand(string Id): IRequest<CarEntity?>;