namespace RaceIntel.Api.Nascar.Services;

public class NascarPollingService : BackgroundService
{
    private readonly NascarApiClient _apiClient;
    private readonly NascarCacheService _cache;
    private readonly ILogger<NascarPollingService> _logger;
    private readonly int _intervalMs;

    //constructor
    public NascarPollingService(
        NascarApiClient apiClient,
        NascarCacheService cache,
        ILogger<NascarPollingService> logger,
        IConfiguration config)
    {
        _apiClient = apiClient;
        _cache = cache;
        _logger = logger;
        _intervalMs = config.GetValue<int>("PollingIntervalMs", 5000);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //log that we're starting
        _logger.LogInformation(
            "NASCAR polling service started (interval: {Interval}ms)",
            _intervalMs);

        //loop forever until cancellation requested
        //(when docker sends SIGTERM, or Ctrl+C in dev)
        while (!stoppingToken.IsCancellationRequested)
        {
            //call the API client to fetch latest data
            var feed = await _apiClient.GetLiveFeedAsync(stoppingToken);

            //if pull is successful
            if (feed is not null)
            {
                //update the cache with fresh data
                _cache.Update(feed);

                //only log in dev
                _logger.LogDebug(
                    "Cache updated - {RaceName} | Lap {Lap}/{Total} | Flag: {Flag}",
                    feed.RaceName, feed.LapNumber, feed.LapNumber + feed.LapsToGo, feed.FlagState);
            }

            //wait until configured interval before next poll
            //will be interrupted immediately if cancellation requested
            await Task.Delay(_intervalMs, stoppingToken);
        }

        //log that we're stopping
        _logger.LogInformation("NASCAR polling service stopped");
    }
}