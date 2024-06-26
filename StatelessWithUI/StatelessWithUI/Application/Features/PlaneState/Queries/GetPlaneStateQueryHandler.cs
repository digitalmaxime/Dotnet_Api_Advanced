using MediatR;
using StatelessWithUI.Application.Contracts;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public class GetPlaneStateQueryHandler: IRequestHandler<GetPlaneStateQuery, GetPlaneStateQueryResponseDto?>
{
    private readonly IPlaneStateRepository _planeStateRepository;

    public GetPlaneStateQueryHandler(IPlaneStateRepository planeStateRepository)
    {
        _planeStateRepository = planeStateRepository;
    }

    public async Task<GetPlaneStateQueryResponseDto?> Handle(GetPlaneStateQuery request, CancellationToken cancellationToken)
    {
        var result = await _planeStateRepository.GetState(request.Id, request.PlaneState);
        
        if (result == null) return null;
        
        return new GetPlaneStateQueryResponseDto()
        {
            StateName = result.StateName,
            StateId = result.Id
        };
    }
}