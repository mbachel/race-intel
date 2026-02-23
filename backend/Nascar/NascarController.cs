namespace RaceIntel.Api.Nascar;

using Microsoft.AspNetCore.Mvc;
using RaceIntel.Api.Nascar.Services;

[ApiController]

[Route("api/nascar")]

public class NascarController : ControllerBase
{
    private readonly NascarCacheService _cache;

    //constructor
    public NascarController(NascarCacheService cache)
    {
        _cache = cache;
    }

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