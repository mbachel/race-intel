namespace RaceIntel.Api.Nascar;

using Microsoft.AspNetCore.Mvc;
using RaceIntel.Api.Nascar.Services;

/// <summary>Exposes NASCAR live feed endpoints.</summary>
[ApiController]
[Route("api/nascar")]
public class NascarController : ControllerBase
{
    private readonly NascarCacheService _cache;

    /// <summary>Initializes a new instance of the <see cref="NascarController"/> class.</summary>
    /// <param name="cache">Cache service for NASCAR live feed data.</param>
    public NascarController(NascarCacheService cache)
    {
        _cache = cache;
    }

    /// <summary>Gets the latest live NASCAR feed snapshot.</summary>
    /// <returns>Live feed data or a waiting status when unavailable.</returns>
    [HttpGet("live")]
    public IActionResult GetLiveFeed()
    {
        var (feed, lastUpdated) = _cache.GetLatest();

        if (feed is null)
        {
            return Ok(new
            {
                status = "waiting",
                message = "No live data available yet. Either no race is active or the first poll hasn't completed. Please check back shortly."
            });
        }

        return Ok(new
        {
            status = "live",
            lastUpdated,
            data = feed
        });
    }
}