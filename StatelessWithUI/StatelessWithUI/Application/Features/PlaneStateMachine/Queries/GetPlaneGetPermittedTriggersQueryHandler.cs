using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public class GetPlaneGetPermittedTriggersQueryHandler: IRequestHandler<GetPlaneGetPermittedTriggersQuery, IEnumerable<string>?>
{
    private readonly IPlaneService _planeService;

    public GetPlaneGetPermittedTriggersQueryHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public Task<IEnumerable<string>?> Handle(GetPlaneGetPermittedTriggersQuery request, CancellationToken cancellationToken)
    {
        return _planeService.GetPermittedTriggers(request.Id);
    }
}