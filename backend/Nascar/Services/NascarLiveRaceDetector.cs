namespace RaceIntel.Api.Nascar.Services;

using RaceIntel.Api.Nascar.Models;

/// <summary>Detects live NASCAR race activity based on feed changes.</summary>
public class NascarLiveRaceDetector
{
    //init api client and logger
    private readonly NascarApiClient _apiClient;
    private readonly ILogger<NascarLiveRaceDetector> _logger;

    //lock and state for tracking feed changes
    private readonly object _lock = new();
    private int? _lastElapsedTime;
    private int? _lastLapNumber;
    private int? _lastTimeOfDay;
    private DateTime _lastChangeAtUtc = DateTime.MinValue;

    /// <summary>Initializes a new instance of the <see cref="NascarLiveRaceDetector"/> class.</summary>
    /// <param name="apiClient">API client for live feed retrieval.</param>
    /// <param name="logger">Logger for detection details.</param>
    public NascarLiveRaceDetector(NascarApiClient apiClient, ILogger<NascarLiveRaceDetector> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    //define race state:
    // unknown = feed unreachable, 
    // idle = feed reachable but no advancement, 
    // active = feed advancing
    /// <summary>Represents the detected race activity state.</summary>
    public enum RaceActivityState
    {
        /// <summary>Feed unavailable or unreachable.</summary>
        Unknown,
        /// <summary>Feed reachable but not advancing.</summary>
        Idle,
        /// <summary>Feed advancing, indicating live activity.</summary>
        Active
    }

    //record for returning status with reason and next check delay
    /// <summary>Provides race activity status with context and next check delay.</summary>
    /// <param name="State">Detected activity state.</param>
    /// <param name="NextCheckDelay">Delay until the next status check.</param>
    /// <param name="Feed">Latest feed data, when available.</param>
    /// <param name="Reason">Reason for the current state.</param>
    public record LiveRaceStatus(
        RaceActivityState State,
        TimeSpan NextCheckDelay,
        LiveFeedResponse? Feed,
        string Reason);

    //main method to check live race status
    /// <summary>Gets the current live race status based on feed changes.</summary>
    /// <param name="ct">Cancellation token for the request.</param>
    /// <returns>The current live race status.</returns>
    public async Task<LiveRaceStatus> GetStatusAsync(CancellationToken ct)
    {
        //init feed. If fetch fails, return unknown status with reason
        var feed = await _apiClient.GetLiveFeedAsync(ct);

        //if feed is null, return unknown status with reason
        if (feed is null)
        {
            return new LiveRaceStatus(
                State: RaceActivityState.Unknown,
                NextCheckDelay: TimeSpan.FromMinutes(2),
                Feed: null,
                Reason: "Feed fetch failed/null"
            );
        }

        //extract key metrics for change detection
        var elapsed = feed.ElapsedTime;
        var lap = feed.LapNumber;
        var tod = feed.TimeOfDay;

        //lock to ensure thread safety of state checks and updates
        lock (_lock)
        {
            //first observation: init state, assume inactive
            if (_lastElapsedTime is null && _lastLapNumber is null && _lastTimeOfDay is null)
            {
                _lastElapsedTime = elapsed;
                _lastLapNumber = lap;
                _lastTimeOfDay = tod;
                _lastChangeAtUtc = DateTime.UtcNow;

                //log that we've initialized baseline
                return new LiveRaceStatus(
                    State: RaceActivityState.Idle,
                    NextCheckDelay: TimeSpan.FromSeconds(30),
                    Feed: feed,
                    Reason: "Initialized baseline. Waiting for advancement"
                );
            }

            var advanced = 
                elapsed > (_lastElapsedTime ?? int.MinValue) ||
                lap > (_lastLapNumber ?? int.MinValue) ||
                tod > (_lastTimeOfDay ?? int.MinValue);

            if (advanced)
            {
                _lastElapsedTime = elapsed;
                _lastLapNumber = lap;
                _lastTimeOfDay = tod;
                _lastChangeAtUtc = DateTime.UtcNow;

                return new LiveRaceStatus(
                    State: RaceActivityState.Active,
                    NextCheckDelay: TimeSpan.FromSeconds(30),
                    Feed: feed,
                    Reason: "Feed advanced (elapsed/lap/time_of_day increased)"
                );
            }

            //calculate how long feed has been frozen (not advanced) for,
            //to determine if we should check more or less frequently
            var frozenFor = DateTime.UtcNow - _lastChangeAtUtc;
            
            //if frozen for less than 5 minutes, consider it idle and check again soon.
            //if frozen for longer, still idle but check less frequently to avoid unnecessary load during off-weeks
            if (frozenFor < TimeSpan.FromMinutes(5))
            {
                return new LiveRaceStatus(
                    State: RaceActivityState.Idle,
                    NextCheckDelay: TimeSpan.FromSeconds(30),
                    Feed: feed,
                    Reason: $"Feed did not advance. Frozen for {frozenFor.TotalSeconds:n0}s"
                );
            }

            //frozen for a long time, still idle but check less frequently
            return new LiveRaceStatus(
                State: RaceActivityState.Idle,
                NextCheckDelay: TimeSpan.FromMinutes(10),
                Feed: feed,
                Reason: $"Feed did not advance. Frozen for {frozenFor.TotalSeconds:n0}s. Will check less frequently."
            );
        }
    }
}