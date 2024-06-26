using Microsoft.EntityFrameworkCore;
using StatelessWithUI.Persistence.Domain;
using StatelessWithUI.Persistence.Domain.PlaneStates;
using StatelessWithUI.VehicleStateMachines;
using StatelessWithUI.VehicleStateMachines.CarStateMachine;
using StatelessWithUI.VehicleStateMachines.PlaneStateMachine;

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
        modelBuilder.Entity<PlaneEntity>()
            .HasMany<StateBase>(x => x.PlaneStates)
            .WithOne(x => x.PlaneEntity)
            .HasForeignKey(x => x.PlaneEntityId);

        // modelBuilder.Entity<PlaneEntity>()
        //     .HasMany<InitialState>(x => x.InitialStates)
        //     .WithOne(x => x.PlaneEntity)
        //     .HasForeignKey(x => x.PlaneEntityId);

        modelBuilder.Entity<BuildTask>()
            .HasOne(x => x.BuildState)
            .WithMany(x => x.BuildTasks)
            .HasForeignKey(x => x.BuildStateId);

        modelBuilder.Entity<PlaneEntity>()
            .HasData(new PlaneEntity()
            {
                Id = "1",
                // CurrentStateEnumName = PlaneStateMachine.PlaneState.DesignState.ToString()
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

        modelBuilder.Entity<BuildState>()
            .HasOne(x => x.PlaneEntity)
            // .WithMany(x => x.PlaneStates)
            // .WithMany(x => x.BuildStates)
            // .HasForeignKey(x => x.PlaneEntityId)
            ;

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