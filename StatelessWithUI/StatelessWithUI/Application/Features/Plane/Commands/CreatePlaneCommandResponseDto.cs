namespace StatelessWithUI.Application.Features.Plane.Commands;

public class CreatePlaneCommandResponseDto
{
    public string PlaneId { get; set; }
    public IEnumerable<CreatePlaneCommandPlaneStateDto> PlaneStateDtos { get; set; }
}

public class CreatePlaneCommandPlaneStateDto
{
    public string StateId { get; set; }
    public string StateName { get; set; }
    public bool IsComplete { get; set; }
}