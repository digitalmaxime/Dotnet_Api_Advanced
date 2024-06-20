using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines;
using StatelessWithUI.VehicleStateMachines.CarStateMachine;
using StatelessWithUI.VehicleStateMachines.CarStateMachine.CarStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;

namespace StatelessWithUI.Persistence;

public class VehicleDbContext : DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("VehicleState");
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<VehicleEntityBase>()
        //     .Property(x => x.StateEnum)
        //     .HasConversion<string>();
        
        modelBuilder.Entity<StateBase>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<BuildState>()
            .Ignore(x => x.Graph);
        
        modelBuilder.Entity<BuildTask>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<CarSnapshotEntity>()
            .HasData(new CarSnapshotEntity()
                {
                    Id = "Id1",
                    HorsePower = 0,
                    StateId = "StateId1",
                    CurrentStateEnumName = CarStateMachine.CarState.InitialState.ToString()
                },
                new CarSnapshotEntity()
                {
                    Id = "Id2",
                    HorsePower = 0,
                    StateId = "StateId2",
                    CurrentStateEnumName = CarStateMachine.CarState.InitialState.ToString()
                });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CarSnapshotEntity> CarEntity { get; set; } = default!;
    public DbSet<PlaneSnapshotEntity> PlaneEntity { get; set; } = default!;
    public DbSet<InitialState> InitialState { get; set; } = default!;
    public DbSet<DesignState> DesignState { get; set; } = default!;
    public DbSet<BuildState> BuildState { get; set; } = default!;
    public DbSet<TestingState> TestingState { get; set; } = default!;
    public DbSet<BuildTask> BuildTask { get; set; } = default!;
}