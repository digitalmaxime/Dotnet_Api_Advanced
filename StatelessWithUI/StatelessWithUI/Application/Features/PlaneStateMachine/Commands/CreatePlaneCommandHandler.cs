using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public class CreatePlaneCommandHandler : IRequestHandler<CreatePlaneCommand, EntityBase?>
{
    private readonly IPlaneService _planeService;

    public CreatePlaneCommandHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<EntityBase?> Handle(CreatePlaneCommand request, CancellationToken cancellationToken)
    {
        return await _planeService.CreateAsync(request.Id);
    }
}