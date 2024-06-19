using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public class TakePlaneActionCommandHandler : IRequestHandler<TakePlaneActionCommand, bool>
{
    private readonly IPlaneService _planeService;

    public TakePlaneActionCommandHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<bool> Handle(TakePlaneActionCommand request, CancellationToken cancellationToken)
    {
        return _planeService.TakeAction(request.Id, request.Action).GetAwaiter().GetResult();
    }
}