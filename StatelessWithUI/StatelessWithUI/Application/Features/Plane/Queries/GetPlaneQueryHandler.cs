using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Queries;

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