using MediatR;
using StatelessWithUI.Persistence.Contracts;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public class GetAllPlaneStatesQueryHandler: IRequestHandler<GetAllPlaneStatesQuery, GetAllPlaneStatesQueryResponseDto?>
{
    private readonly IPlaneStateRepository _planeStateRepository;

    public GetAllPlaneStatesQueryHandler(IPlaneStateRepository planeStateRepository)
    {
        _planeStateRepository = planeStateRepository;
    }
    
    public async Task<GetAllPlaneStatesQueryResponseDto?> Handle(GetAllPlaneStatesQuery request, CancellationToken cancellationToken)
    {
        var result = (await _planeStateRepository.GetAllStates(request.PlaneState))?.ToList();

        if (result == null || !result.Any()) return null;

        return new GetAllPlaneStatesQueryResponseDto()
        {
            StateName = result[0].GetStateName(),
            StatePlanePairs = result.Select(x => new StatePlanePair(x.Id, x.PlaneEntityId))
        };
    }
}