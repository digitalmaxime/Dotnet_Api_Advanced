namespace StatelessWithUI.Application.Features.PlaneState.Commands;

public class CreatePlaneBuildStateCommandResponseDto
{
    public string PlaneId { get; set; }
    public string StateId { get; set; }
    public string StateName { get; set; }
}