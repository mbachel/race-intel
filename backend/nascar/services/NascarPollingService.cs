namespace RaceIntel.Api.Nascar.Services;

public class NascarPollingService : BackgroundService
{
    private readonly NascarCacheService _cache;
    private readonly NascarLiveRaceDetector _detector;
    private readonly ILogger<NascarPollingService> _logger;

    //constructor
    public NascarPollingService(
        NascarLiveRaceDetector detector,
        NascarCacheService cache,
        ILogger<NascarPollingService> logger)
    {
        _detector = detector;
        _cache = cache;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //log that we're starting
        _logger.LogInformation("NASCAR polling service started (gated via live-race detector)");

        //loop forever until cancellation requested
        //(when docker sends SIGTERM, or Ctrl+C in dev)
        while (!stoppingToken.IsCancellationRequested)
        {
            //call the live race detector to check the status
            var status = await _detector.GetStatusAsync(stoppingToken);

            //if the feed is active, fetch latest data
            if (status.State == NascarLiveRaceDetector.RaceActivityState.Active && status.Feed is not null)
            {
                //update the cache with fresh data
                _cache.Update(status.Feed);
            }

            _logger.LogDebug("NASCAR detector state: {State} ({Reason}). Next check in {Delay}",
                status.State, status.Reason, status.NextCheckDelay);

            //wait until configured interval before next poll
            //will be interrupted immediately if cancellation requested
            await Task.Delay(status.NextCheckDelay, stoppingToken);
        }

        //log that we're stopping
        _logger.LogInformation("NASCAR polling service stopped");
    }
}