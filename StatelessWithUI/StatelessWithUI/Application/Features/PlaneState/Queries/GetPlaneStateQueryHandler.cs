using MediatR;
using StatelessWithUI.Persistence.Contracts;

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
            StateName = result.GetStateName(),
            StateId = result.Id
        };
    }
}