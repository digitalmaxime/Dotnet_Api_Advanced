using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.VehicleStateMachines;
using StatelessWithUI.VehicleStateMachines.CarStateMachine;
using StatelessWithUI.VehicleStateMachines.CarStateMachine.CarStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.BuildState;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.DesignState;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.PlaneStates;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine.TestState;

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
        modelBuilder.Entity<VehicleStateBase>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<CarEntity>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<PlaneEntity>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<PlaneEntity>()
            .HasOne<VehicleStateBase>(x => x.State);

        modelBuilder.Entity<PlaneEntity>()
            .HasOne<VehicleStateBase>(x => x.State);
        
        modelBuilder.Entity<BuildTask>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CarEntity>()
            .HasData(new CarEntity()
                {
                    Id = "Id1",
                    HorsePower = 0,
                    StateId = "StateId1"
                },
                new CarEntity()
                {
                    Id = "Id2",
                    HorsePower = 0,
                    StateId = "StateId2"
                });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CarEntity> CarEntity { get; set; } = default!;
    public DbSet<PlaneEntity> PlaneEntity { get; set; } = default!;
    public DbSet<VehicleInitialState> VehicleInitialState { get; set; } = default!;
    public DbSet<DesignState> DesignState { get; set; } = default!;
    public DbSet<BuildState> BuildState { get; set; } = default!;
    public DbSet<TestingState> TestingState { get; set; } = default!;
    public DbSet<BuildTask> BuildTask { get; set; } = default!;
}