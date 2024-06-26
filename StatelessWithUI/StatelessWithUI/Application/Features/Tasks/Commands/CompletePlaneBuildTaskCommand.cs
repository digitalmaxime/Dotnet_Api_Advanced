using MediatR;

namespace StatelessWithUI.Application.Features.Tasks.Commands;

public record CompletePlaneBuildTaskCommand(string Id) : IRequest<CompletePlaneBuildTaskCommandResponseDto?>;