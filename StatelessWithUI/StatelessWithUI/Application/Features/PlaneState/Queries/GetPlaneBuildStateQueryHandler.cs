using MediatR;
using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Application.Features.PlaneState.Queries;

public class
    GetPlaneBuildStateQueryHandler : IRequestHandler<GetPlaneBuildStateQuery, GetPlaneBuildStateQueryResponseDto?>
{
    private readonly IPlaneStateRepository _planeStateRepository;

    public GetPlaneBuildStateQueryHandler(IPlaneStateRepository planeStateRepository)
    {
        _planeStateRepository = planeStateRepository;
    }

    public async Task<GetPlaneBuildStateQueryResponseDto?> Handle(GetPlaneBuildStateQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _planeStateRepository.GetBuildState(request.Id);

        if (result == null) return null;
        return new GetPlaneBuildStateQueryResponseDto()
        {
            StateName = result.GetStateName(),
            StateId = result.Id,
            BuildTaskNodes = new List<BuildTaskNode>()
            {
                new BuildTaskNode()
                {
                    BuildTask = result.BuildEngines,
                    Dependencies = result.Graph.GetDependencies(result.BuildEngines)
                },
                new BuildTaskNode()
                {
                    BuildTask = result.BuildSoftware,
                    Dependencies = result.Graph.GetDependencies(result.BuildSoftware)
                },
                new BuildTaskNode()
                {
                    BuildTask = result.BuildWings,
                    Dependencies = result.Graph.GetDependencies(result.BuildWings)
                },
                new BuildTaskNode()
                {
                    BuildTask = result.GetMaterials,
                    Dependencies = result.Graph.GetDependencies(result.GetMaterials)
                },
                new BuildTaskNode()
                {
                    BuildTask = result.IntegrateParts,
                    Dependencies = result.Graph.GetDependencies(result.IntegrateParts)
                },
                new BuildTaskNode()
                {
                    BuildTask = result.AssembleWingsEngines,
                    Dependencies = result.Graph.GetDependencies(result.AssembleWingsEngines)
                }
            }
        };
    }

}