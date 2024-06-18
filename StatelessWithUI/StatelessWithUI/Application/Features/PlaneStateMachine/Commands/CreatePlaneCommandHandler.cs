using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Application.Features.PlaneStateMachine.Commands;

public class CreatePlaneCommandHandler : IRequestHandler<CreatePlaneCommand, bool>
{
    // Inject your repository or service that will handle the creation of the PlaneEntity
    private readonly IPlaneStateRepository _planeService;

    public CreatePlaneCommandHandler(IPlaneStateRepository planeService)
    {
        _planeService = planeService;
    }

    public async Task<bool> Handle(CreatePlaneCommand request, CancellationToken cancellationToken)
    {
        var plane = new PlaneEntity
        {
            Id = request.Id,
            Speed = request.Speed,
            State = request.State
        };

        return await _planeService.Save(plane);
    }
}