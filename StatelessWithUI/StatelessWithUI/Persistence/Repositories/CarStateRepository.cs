using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class CarStateRepository : EntityWithIdRepository<CarEntity>, ICarStateRepository
{
    public CarStateRepository(VehicleDbContext dbContext): base(dbContext)
    {
        Console.WriteLine("CarStateRepository created");
    }
    
    ~CarStateRepository()
    {
        Console.WriteLine("CarStateRepository destroyed");
    }
}