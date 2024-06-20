using StatelessWithUI.Persistence.Contracts;
using StatelessWithUI.Persistence.Domain;

namespace StatelessWithUI.Persistence.Repositories;

public class CarRepository : EntityWithIdRepository<CarSnapshotEntity>, ICarRepository
{
    public CarRepository(VehicleDbContext dbContext): base(dbContext)
    {
        Console.WriteLine("CarStateRepository created");
    }
    
    ~CarRepository()
    {
        Console.WriteLine("CarStateRepository destroyed");
    }
}