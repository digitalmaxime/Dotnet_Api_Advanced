using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.Plane.Queries;

public class GetPlaneQueryHandler : IRequestHandler<GetPlaneQuery, GetPlaneQueryResponseDto?>
{
    private readonly IPlaneService _planeService;

    public GetPlaneQueryHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<GetPlaneQueryResponseDto?> Handle(GetPlaneQuery request, CancellationToken cancellationToken)
    {
        return await _planeService.GetPlaneEntity(request.Id, request.includes);
    }
}