using MediatR;
using StatelessWithUI.Application.Features.PlaneStateMachine.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

public class GetPlaneQueryHandler : IRequestHandler<GetPlaneQuery, PlaneEntity?>
{
    private readonly IPlaneService _planeService;

    public GetPlaneQueryHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<PlaneEntity?> Handle(GetPlaneQuery request, CancellationToken cancellationToken)
    {
        return await _planeService.GetPlaneEntity(request.Id);
    }
}