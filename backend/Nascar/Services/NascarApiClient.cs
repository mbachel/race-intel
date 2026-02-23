namespace RaceIntel.Api.Nascar.Services;

using System.Text.Json;
using RaceIntel.Api.Nascar.Models;

public class NascarApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<NascarApiClient> _logger;

    //store public access link to live feed json
    private const string LiveFeedUrl = "https://cf.nascar.com/live/feeds/live-feed.json";

    //constructor
    public NascarApiClient(HttpClient http, ILogger<NascarApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<LiveFeedResponse?> GetLiveFeedAsync(CancellationToken ct = default)
    {
        try
        {
            var response = await _http.GetAsync(LiveFeedUrl, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "NASCAR live feed returned {StatusCode}",
                    response.StatusCode);
                return null;
            }

            var json = await response.Content.ReadAsStringAsync(ct);

            return JsonSerializer.Deserialize<LiveFeedResponse>(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch NASCAR live feed");
            return null;
        }
    }
}