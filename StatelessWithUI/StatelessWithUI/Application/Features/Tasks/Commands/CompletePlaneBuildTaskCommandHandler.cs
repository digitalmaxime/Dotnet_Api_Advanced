using MediatR;
using StatelessWithUI.Application.Contracts;

namespace StatelessWithUI.Application.Features.Tasks.Commands;

public class CompletePlaneBuildTaskCommandHandler : IRequestHandler<CompletePlaneBuildTaskCommand, CompletePlaneBuildTaskCommandResponseDto?>
{
    private readonly IBuildTaskRepository _buildTaskRepository;
    public CompletePlaneBuildTaskCommandHandler(IBuildTaskRepository buildTaskRepository)
    {
        _buildTaskRepository = buildTaskRepository;
    }

    public async Task<CompletePlaneBuildTaskCommandResponseDto?> Handle(CompletePlaneBuildTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _buildTaskRepository.CompleteTask(request.Id);
        return new CompletePlaneBuildTaskCommandResponseDto()
        {
            Success = result
        };
    }
}