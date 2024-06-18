using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public class TakePlaneActionCommandHandler : IRequestHandler<TakePlaneActionCommand>
{
    private readonly IPlaneService _planeService;

    public TakePlaneActionCommandHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task Handle(TakePlaneActionCommand request, CancellationToken cancellationToken)
    {
        _planeService.TakeAction(request.Id, request.Action);
    }
}