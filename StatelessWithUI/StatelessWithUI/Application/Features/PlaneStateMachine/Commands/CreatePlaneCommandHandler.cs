using MediatR;
using StatelessWithUI.Application.Features.PlaneStateMachine.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public class CreatePlaneCommandHandler : IRequestHandler<CreatePlaneCommand, EntityWithId?>
{
    private readonly IPlaneService _planeService;

    public CreatePlaneCommandHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<EntityWithId?> Handle(CreatePlaneCommand request, CancellationToken cancellationToken)
    {
        return await _planeService.CreateAsync(request.Id);
    }
}