namespace RaceIntel.Api.Data;

using Microsoft.EntityFrameworkCore;
using RaceIntel.Api.Data.Entities;

public class RaceIntelDbContext : DbContext
{
    public RaceIntelDbContext(DbContextOptions<RaceIntelDbContext> options) : base(options) { }

    public DbSet<NascarRaceListBasicYear> NascarRaceListBasicYears => Set<NascarRaceListBasicYear>();
    public DbSet<NascarWeekendFeed> NascarWeekendFeeds => Set<NascarWeekendFeed>();

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