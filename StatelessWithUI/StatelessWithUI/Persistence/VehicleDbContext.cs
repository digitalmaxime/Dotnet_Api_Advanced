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
        // modelBuilder.Entity<PlaneEntity>()
        //     .HasMany<StateBase>()
        //     .WithOne(x => x.PlaneEntity)
        //     .HasForeignKey(x => x.PlaneEntityId);
        modelBuilder.Entity<PlaneEntity>()
            .HasMany<InitialState>(x => x.InitialStates)
            .WithOne(x => x.PlaneEntity)
            .HasForeignKey(x => x.PlaneEntityId);
        // modelBuilder.Entity<StateBase>()
        //     .Ignore(x => x.PlaneEntity);

        // modelBuilder.Entity<PlaneEntity>()
        //     .HasMany<DesignState>(x => x.DesignStates)
        //     .WithOne(x => x.PlaneEntity)
        //     .HasForeignKey(x => x.PlaneEntityId);
        // modelBuilder.Entity<PlaneEntity>()
        //     .HasMany<BuildState>(x => x.BuildStates)
        //     .WithOne(x => x.PlaneEntity)
        //     .HasForeignKey(x => x.PlaneEntityId);
        // modelBuilder.Entity<PlaneEntity>()
        //     .HasMany<TestingState>(x => x.TestingStates)
        //     .WithOne(x => x.PlaneEntity)
        //     .HasForeignKey(x => x.PlaneEntityId);
        //
        // modelBuilder.Entity<BuildState>()
        //     .HasOne(x => x.PlaneEntity)
        //     .WithMany(x => x.States);

        modelBuilder.Entity<BuildState>()
            .Ignore(x => x.Graph);

        modelBuilder.Entity<CarEntity>()
            .HasData(new CarEntity()
                {
                    Id = "Id1",
                    HorsePower = 0,
                    // StateId = "StateId1",
                    CurrentStateEnumName = CarStateMachine.CarState.InitialState.ToString()
                },
                new CarEntity()
                {
                    Id = "Id2",
                    HorsePower = 0,
                    // StateId = "StateId2",
                    CurrentStateEnumName = CarStateMachine.CarState.InitialState.ToString()
                });

        modelBuilder.Entity<PlaneEntity>()
            .HasData(new PlaneEntity()
            {
                Id = "1",
                CurrentStateEnumName = PlaneStateMachine.PlaneState.DesignState.ToString()
            });

        modelBuilder.Entity<InitialState>()
            .HasData(new List<InitialState>()
            {
                new InitialState()
                {
                    Id = "InitialStateId1",
                    PlaneEntityId = "1"
                }
            });
        
        modelBuilder.Entity<DesignState>()
            .HasData(new List<DesignState>()
            {
                new DesignState()
                {
                    Id = "DesignStateId1",
                    PlaneEntityId = "1"
                }
            });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CarEntity> CarEntity { get; set; } = default!;
    public DbSet<PlaneEntity> PlaneEntity { get; set; } = default!;
    public DbSet<InitialState> InitialState { get; set; } = default!;
    public DbSet<DesignState> DesignState { get; set; } = default!;
    public DbSet<BuildState> BuildState { get; set; } = default!;
    public DbSet<TestingState> TestingState { get; set; } = default!;
    public DbSet<BuildTask> BuildTask { get; set; } = default!;
}