using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Plane.Queries;

public class GetAllPlaneQueryHandler : IRequestHandler<GetAllPlaneQuery, GetAllPlaneQueryResponseDto?>
{
    private readonly IPlaneService _planeService;

    public GetAllPlaneQueryHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<GetAllPlaneQueryResponseDto?> Handle(GetAllPlaneQuery request, CancellationToken cancellationToken)
    {
        var res = await _planeService.GetAllPlanes();
        if (res == null) return null;
        return new GetAllPlaneQueryResponseDto()
        {
            GetAllPlaneQueryDto = res.Select(x => new AllPlaneDto()
            {
                PlaneId = x.Id,
                PlaneStateDtos = x.PlaneStates.Select(y => new GetAllPlaneQueryPlaneStateDto()
                {
                    StateId = y.Id,
                    StateName = y.StateName,
                    IsComplete = y.IsStateComplete
                })
            })
        };
    }
}
