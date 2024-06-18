using MediatR;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Controllers;

public record GetCarByIdQuery(string Id) : IRequest<CarEntity?>;