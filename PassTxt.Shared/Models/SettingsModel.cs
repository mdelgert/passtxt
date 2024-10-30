namespace PassTxt.Shared.Models;

public class SettingsModel
{
    private const int DefaultTimeoutInSeconds = 5;
    private const int MillisecondsMultiplier = 1000;

    private int _timeoutInSeconds = DefaultTimeoutInSeconds * MillisecondsMultiplier;

    // Timeout in seconds with fallback to a default value (multiplied by milliseconds)
    public int TimeoutInSeconds
    {
        get => _timeoutInSeconds;
        set => _timeoutInSeconds = value > 0
            ? value * MillisecondsMultiplier
            : DefaultTimeoutInSeconds * MillisecondsMultiplier;
    }

    public bool IsFeatureEnabled { get; set; } = false;
    public bool IsEnabled { get; set; } = true;
    public string Message { get; set; } = "HelloWorld!";
    public int MessageCount { get; set; } = 1;
}
