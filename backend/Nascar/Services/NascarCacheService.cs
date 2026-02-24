namespace RaceIntel.Api.Nascar.Services;

using RaceIntel.Api.Nascar.Models;

/// <summary>Caches the latest NASCAR live feed snapshot for quick access.</summary>
public class NascarCacheService
{
    //store the latest feed as a LiveFeedResponse object
    private LiveFeedResponse? _latestFeed;
    //store timestamp of last update
    private DateTime _lastUpdated;

    //lock object for thread safety
    private readonly object _lock = new();

    /// <summary>Updates the cached live feed snapshot.</summary>
    /// <param name="feed">Latest feed data to cache.</param>
    public void Update(LiveFeedResponse feed)
    {
        lock (_lock)
        {
            _latestFeed = feed;
            _lastUpdated = DateTime.UtcNow;
        }
    }

    /// <summary>Gets the latest cached feed and its update timestamp.</summary>
    /// <returns>The cached feed and the last updated time in UTC.</returns>
    public (LiveFeedResponse? Feed, DateTime LastUpdated) GetLatest()
    {
        lock (_lock)
        {
            return (_latestFeed, _lastUpdated);
        }
    }
}