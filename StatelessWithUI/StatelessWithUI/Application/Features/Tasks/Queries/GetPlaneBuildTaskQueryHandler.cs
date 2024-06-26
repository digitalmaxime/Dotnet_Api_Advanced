using MediatR;
using StatelessWithUI.Controllers;
using StatelessWithUI.Persistence.Contracts;

namespace StatelessWithUI.Application.Features.Tasks.Queries;

public class GetPlaneBuildTaskQueryHandler : IRequestHandler<GetPlaneBuildTaskQuery, GetPlaneBuildTaskQueryResponseDto?>
{
    private readonly IBuildTaskRepository _buildTaskRepository;

    public GetPlaneBuildTaskQueryHandler(IBuildTaskRepository buildTaskRepository)
    {
        _buildTaskRepository = buildTaskRepository;
    }

    public async Task<GetPlaneBuildTaskQueryResponseDto?> Handle(GetPlaneBuildTaskQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _buildTaskRepository.GetTaskByIdAsync(request.Id);

        if (result == null) return null;

        return new GetPlaneBuildTaskQueryResponseDto()
        {
            Id = result.Id,
            IsComplete = result.IsComplete,
            VehicleId = "result."
        };
    }
}