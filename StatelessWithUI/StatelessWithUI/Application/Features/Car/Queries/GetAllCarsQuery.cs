using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Car.Queries;

public record GetAllCarsQuery: IRequest<IEnumerable<CarEntity>>;