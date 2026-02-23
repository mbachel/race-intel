namespace RaceIntel.Api.Admin.Import;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceIntel.Api.Data;
using RaceIntel.Api.Data.Entities;
using RaceIntel.Api.Nascar.Services;

[ApiController]
[Route("api/admin/import/nascar")]
public class NascarImportController : ControllerBase
{
    private readonly RaceIntelDbContext _db;
    private readonly NascarHistoricalApiClient _historical;
    private readonly ILogger<NascarImportController> _logger;
    
    public NascarImportController(
        RaceIntelDbContext db, 
        NascarHistoricalApiClient historical, 
        ILogger<NascarImportController> logger)
    {
        _db = db;
        _historical = historical;
        _logger = logger;
    }

    [HttpPost("race-list-basic")]
    public async Task<IActionResult> ImportRaceListBasic(
        [FromBody] NascarRaceListBasicImportRequest req, 
        CancellationToken ct)
    {
        try
        {
            var existing = await _db.NascarRaceListBasicYears
                .SingleOrDefaultAsync(x => x.Year == req.Year, ct);

            if (existing is not null && !req.Force)
            {
                return Ok(new 
                { 
                    status = "skipped", 
                    reason = "Already imported", 
                    year = req.Year, 
                    pulledAtUtc = existing.PulledAtUtc 
                });
            }

            var rawJson = await _historical.GetRaceListBasicRawJsonAsync(req.Year, ct);

            if (existing is null)
            {
                _db.NascarRaceListBasicYears.Add(new NascarRaceListBasicYear
                {
                    Year = req.Year,
                    PulledAtUtc = DateTime.UtcNow,
                    RawJson = rawJson
                });
            }
            else
            {
                existing.PulledAtUtc = DateTime.UtcNow;
                existing.RawJson = rawJson;
            }

            await _db.SaveChangesAsync(ct);

            return Ok(new { status = "imported", type = "race-list-basic", year = req.Year, pulledAtUtc = DateTime.UtcNow });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Race list basic import failed for year {Year}", req.Year);
            return StatusCode(StatusCodes.Status502BadGateway, new
            {
                status = "failed",
                type = "race-list-basic",
                year = req.Year,
                error = ex.Message
            });
        }
    }

    [HttpPost("weekend-feed")]
    public async Task<IActionResult> ImportWeekendFeed(
        [FromBody] NascarWeekendFeedImportRequest req,
        CancellationToken ct)
    {
        try
        {
            var existing = await _db.NascarWeekendFeeds
                .SingleOrDefaultAsync(x =>
                    x.Year == req.Year &&
                    x.SeriesId == req.SeriesId &&
                    x.RaceId == req.RaceId, ct);

            if (existing is not null && !req.Force)
            {
                return Ok(new
                {
                    status = "skipped",
                    reason = "Already imported",
                    year = req.Year,
                    seriesId = req.SeriesId,
                    raceId = req.RaceId,
                    pulledAtUtc = existing.PulledAtUtc
                });
            }

            var rawJson = await _historical.GetWeekendFeedRawJsonAsync(req.Year, req.SeriesId, req.RaceId, ct);

            if (existing is null)
            {
                _db.NascarWeekendFeeds.Add(new NascarWeekendFeed
                {
                    Year = req.Year,
                    SeriesId = req.SeriesId,
                    RaceId = req.RaceId,
                    PulledAtUtc = DateTime.UtcNow,
                    RawJson = rawJson
                });
            }
            else
            {
                existing.PulledAtUtc = DateTime.UtcNow;
                existing.RawJson = rawJson;
            }

            await _db.SaveChangesAsync(ct);

            return Ok(new
            {
                status = "imported",
                type = "weekend-feed",
                year = req.Year,
                seriesId = req.SeriesId,
                raceId = req.RaceId,
                pulledAtUtc = DateTime.UtcNow
            });
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, 
                "Weekend feed import failed for year {Year}, series {SeriesId}, race {RaceId}", 
                req.Year, req.SeriesId, req.RaceId);

            return StatusCode(StatusCodes.Status502BadGateway, new
            {
                status = "failed",
                type = "weekend-feed",
                year = req.Year,
                seriesId = req.SeriesId,
                raceId = req.RaceId,
                error = ex.Message
            });
        }
    }
}