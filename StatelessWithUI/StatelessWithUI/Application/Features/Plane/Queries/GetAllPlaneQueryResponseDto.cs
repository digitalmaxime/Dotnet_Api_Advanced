namespace StatelessWithUI.Application.Features.Plane.Queries;

public class GetAllPlaneQueryResponseDto
{
    public IEnumerable<AllPlaneDto> GetAllPlaneQueryDto { get; set; }
}

public class AllPlaneDto
{
    public string PlaneId { get; set; }
    public IEnumerable<GetAllPlaneQueryPlaneStateDto> PlaneStateDtos { get; set; }
}

public class GetAllPlaneQueryPlaneStateDto
{
    public string StateId { get; set; }
    public string StateName { get; set; }
    public bool IsComplete { get; set; }
}