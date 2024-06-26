using MediatR;
using StatelessWithUI.Application.Services;

namespace StatelessWithUI.Application.Features.Tasks.Commands;

public class CreatePlaneBuildTaskCommandHandler : IRequestHandler<CreatePlaneBuildTaskCommandDto, CreatePlaneBuildTaskCommandResponseDto?>
{
    private readonly ITaskService _taskService;

    public CreatePlaneBuildTaskCommandHandler(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<CreatePlaneBuildTaskCommandResponseDto?> Handle(CreatePlaneBuildTaskCommandDto request, CancellationToken cancellationToken)
    {
        var res = await _taskService.CreatePlaneBuildTaskAsync(request.planeStateId, request.TaskName);

        if (res == null) return null;

        return new CreatePlaneBuildTaskCommandResponseDto(res.Id, res.TaskName, res.IsComplete, res.BuildStateId);
    }
}