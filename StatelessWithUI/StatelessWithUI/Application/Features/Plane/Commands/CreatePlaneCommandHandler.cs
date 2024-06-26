using MediatR;
using StatelessWithUI.Application.Services;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.Plane.Commands;

public class CreatePlaneCommandHandler : IRequestHandler<CreatePlaneCommand, CreatePlaneCommandResponseDto?>
{
    private readonly IPlaneService _planeService;

    public CreatePlaneCommandHandler(IPlaneService planeService)
    {
        _planeService = planeService;
    }

    public async Task<CreatePlaneCommandResponseDto?> Handle(CreatePlaneCommand request,
        CancellationToken cancellationToken)
    {
        var res = await _planeService.CreatePlaneAtInitialStateAsync();
        if (res == null) return null;
        return new CreatePlaneCommandResponseDto()
        {
            PlaneId = res.Id,
            PlaneStateDtos = res.PlaneStates.Select(x => new CreatePlaneCommandPlaneStateDto()
                { StateId = x.Id, StateName = x.StateName, IsComplete = x.IsStateComplete })
        };
    }
}