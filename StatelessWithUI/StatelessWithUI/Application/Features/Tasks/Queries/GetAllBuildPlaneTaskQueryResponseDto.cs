using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Application.Features.Tasks.Queries;

public class GetAllBuildPlaneTaskQueryResponseDto
{
    public Dictionary<string, Asdf> BuildStateTasks { get; set; }
}

public class Asdf
{
    public string StateId { get; set; }
    public ICollection<StateTask> BuildStateTasks { get; set; }
    
}
