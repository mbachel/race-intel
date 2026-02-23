namespace RaceIntel.Api.Nascar.Services;

public class NascarHistoricalApiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<NascarHistoricalApiClient> _logger;

    public NascarHistoricalApiClient(HttpClient http, ILogger<NascarHistoricalApiClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<string> GetRaceListBasicRawJsonAsync(int year, CancellationToken ct = default)
    {
        var url = $"https://cf.nascar.com/cacher/{year}/race_list_basic.json";
        return await GetRequiredRawJsonAsync(url, ct);
    }

    public async Task<string> GetWeekendFeedRawJsonAsync(int year, int seriesId, int raceId, CancellationToken ct = default)
    {
        var url = $"https://cf.nascar.com/cacher/{year}/{seriesId}/{raceId}/weekend-feed.json";

        return await GetRequiredRawJsonAsync(url, ct);
    }

    private async Task<string> GetRequiredRawJsonAsync(string url, CancellationToken ct)
    {
        try
        {
            var response = await _http.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch data from {Url}. Status code: {StatusCode}", url, response.StatusCode);
                throw new HttpRequestException(
                    $"Request failed: {(int)response.StatusCode} {response.ReasonPhrase} ({url})"
                );
            }

            var json = await response.Content.ReadAsStringAsync(ct);

            if (string.IsNullOrWhiteSpace(json))
            {
                _logger.LogWarning("Received empty response from {Url}", url);
                throw new HttpRequestException($"Received empty response from {url}");
            }

            return json;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Exception occurred while fetching data from {Url}", url);
            throw;
        }
    }
}