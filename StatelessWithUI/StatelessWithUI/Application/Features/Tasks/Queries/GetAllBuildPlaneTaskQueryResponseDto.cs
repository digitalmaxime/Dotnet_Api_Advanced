using StatelessWithUI.Persistence.Domain.PlaneStates;

namespace StatelessWithUI.Controllers;

public class GetAllBuildPlaneTaskQueryResponseDto
{
    public Dictionary<string, Asdf> BuildStateTasks { get; set; }
}

public class Asdf
{
    public string StateId { get; set; }
    public ICollection<BuildTask> BuildStateTasks { get; set; }
    
}
