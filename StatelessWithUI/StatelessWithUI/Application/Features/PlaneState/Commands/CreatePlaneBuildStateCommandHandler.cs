using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.PlaneState.Commands;

public class CreatePlaneBuildStateCommandHandler : IRequestHandler<CreatePlaneBuildStateCommand, CreatePlaneBuildStateCommandResponseDto?>
{
    private readonly IStateService _stateService;

    public CreatePlaneBuildStateCommandHandler(IStateService stateService)
    {
        _stateService = stateService;
    }

    public async Task<CreatePlaneBuildStateCommandResponseDto?> Handle(CreatePlaneBuildStateCommand request, CancellationToken cancellationToken)
    {
        var res = await _stateService.CreatePlaneStateAsync(request.PlaneId, request.PlaneState);

        if (res == null) return null;

        return new CreatePlaneBuildStateCommandResponseDto()
        {
            StateId = res.Id,
            StateName = res.StateName,
            PlaneId = res.PlaneEntityId
        };

    }
}