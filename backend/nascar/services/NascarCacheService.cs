namespace RaceIntel.Api.Nascar.Services;

using RaceIntel.Api.Nascar.Models;

public class NascarCacheService
{
    //store the latest feed as a LiveFeedResponse object
    private LiveFeedResponse? _latestFeed;
    //store timestamp of last update
    private DateTime _lastUpdated;

    //lock object for thread safety
    private readonly object _lock = new();

    //retrieve latest data, update timestamp of last update
    public void Update(LiveFeedResponse feed)
    {
        lock (_lock)
        {
            _latestFeed = feed;
            _lastUpdated = DateTime.UtcNow;
        }
    }

    //return latest feed and timestamp of last update as tuple
    public (LiveFeedResponse? Feed, DateTime LastUpdated) GetLatest()
    {
        lock (_lock)
        {
            return (_latestFeed, _lastUpdated);
        }
    }
}