using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public class GetAllPlaneQueryHandler : IRequestHandler<GetAllPlaneQuery, IEnumerable<PlaneSnapshotEntity>>
{
    private readonly IPlaneService _planeService;

    public GetAllPlaneQueryHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<IEnumerable<PlaneSnapshotEntity>> Handle(GetAllPlaneQuery request, CancellationToken cancellationToken)
    {
        return await _planeService.GetAll();
    }
}