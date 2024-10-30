namespace PassTxt.Shared.Services;

public class ExampleService(IOptions<SettingsModel> settings, ILogger<ExampleService> logger)
{
    private readonly SettingsModel _settings = settings.Value; // Extract the SettingsModel from IOptions

    public void Hello()
    {
        logger.LogInformation("Message: {Message}", _settings.Message);
    }
}