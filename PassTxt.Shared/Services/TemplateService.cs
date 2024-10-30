namespace PassTxt.Shared.Services;

public class TemplateService(IOptions<SettingsModel> settings, ILogger<TemplateService> logger)
{
    private readonly SettingsModel _settings = settings.Value;

    public void Run()
    {
        // Print settings in JSON format
        PrintSettings();

        // Log information about the settings
        logger.LogInformation("Timeout in milliseconds: {Timeout}", _settings.TimeoutInSeconds);
        logger.LogInformation("Is feature enabled: {IsFeatureEnabled}", _settings.IsFeatureEnabled);

        try
        {
            if (_settings.IsFeatureEnabled)
            {
                logger.LogDebug("Feature is enabled. Executing feature logic...");
            }
            else
            {
                logger.LogDebug("Feature is disabled.");
            }

            // Simulate some work
            Thread.Sleep(_settings.TimeoutInSeconds);
            logger.LogInformation("Completed processing.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during execution");
        }
    }

    // Method to print SettingsModel as JSON
    private void PrintSettings()
    {
        var formattedSettings = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
        {
            WriteIndented = true // Enable formatted (indented) JSON
        });

        logger.LogInformation("Current settings: {Settings}", formattedSettings);
    }
}