namespace PassTxt.Shared.Services;

public class TimeService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TimeService> _logger;

    // Inject HttpClient and ILogger
    public TimeService(HttpClient httpClient, ILogger<TimeService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task GetTimeAsync()
    {
        var url = "http://worldtimeapi.org/api/timezone/Etc/UTC";

        try
        {
            // Make the REST call
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read the JSON response as a string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize and then serialize the JSON to format it (indent it)
            var formattedJson = JsonSerializer.Serialize(
                JsonSerializer.Deserialize<object>(jsonResponse),
                new JsonSerializerOptions { WriteIndented = true });

            // Log the formatted JSON response
            _logger.LogInformation("Time Service Response: {FormattedJson}", formattedJson);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching time from the time service.");
        }
    }
}
