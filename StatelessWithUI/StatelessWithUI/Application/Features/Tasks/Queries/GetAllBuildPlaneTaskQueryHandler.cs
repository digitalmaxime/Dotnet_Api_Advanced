using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Controllers;

public class GetAllBuildPlaneTaskQueryHandler : IRequestHandler<GetAllBuildPlaneTaskQuery, GetAllBuildPlaneTaskQueryResponseDto?>
{
    private readonly ITaskService _taskService;

    public GetAllBuildPlaneTaskQueryHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<GetAllBuildPlaneTaskQueryResponseDto?> Handle(GetAllBuildPlaneTaskQuery request, CancellationToken cancellationToken)
    {
        return await _taskService.GetAllPlaneBuildTasksAsync();
    }
}