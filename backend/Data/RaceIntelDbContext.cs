namespace RaceIntel.Api.Data;

using Microsoft.EntityFrameworkCore;
using RaceIntel.Api.Data.Entities;

/// <summary>Entity Framework Core context for RaceIntel data storage.</summary>
public class RaceIntelDbContext : DbContext
{
    /// <summary>Initializes a new instance of the <see cref="RaceIntelDbContext"/> class.</summary>
    /// <param name="options">EF Core context options.</param>
    public RaceIntelDbContext(DbContextOptions<RaceIntelDbContext> options) : base(options) { }

    /// <summary>Gets the NASCAR race list basic yearly snapshots.</summary>
    public DbSet<NascarRaceListBasicYear> NascarRaceListBasicYears => Set<NascarRaceListBasicYear>();
    /// <summary>Gets the NASCAR weekend feed snapshots.</summary>
    public DbSet<NascarWeekendFeed> NascarWeekendFeeds => Set<NascarWeekendFeed>();

    /// <summary>Configures entity mappings and database schema details.</summary>
    /// <param name="modelBuilder">Builder used to configure the EF model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NascarRaceListBasicYear>(b =>
        {
            b.ToTable("nascar_race_list_basic_year");
            b.HasKey(x => x.Year);
            b.Property(x => x.RawJson).HasColumnType("jsonb");
        });

        modelBuilder.Entity<NascarWeekendFeed>(b =>
        {
            b.ToTable("nascar_weekend_feed");
            b.HasIndex(x => new { x.Year, x.SeriesId, x.RaceId }).IsUnique();
            b.Property(x => x.RawJson).HasColumnType("jsonb");
        });
    }
}