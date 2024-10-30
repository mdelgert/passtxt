using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PassTxt.Shared.Services;
using Serilog;

namespace PassTxt.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        // Allow passing environment via command-line argument or set from environment variables.
        var environment = args.Length > 0 ? args[0].ToLowerInvariant() :
            (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "development").ToLowerInvariant();

        // Build configuration
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        // Configure Serilog from appsettings.json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting the console app");

            // Create a Host for the console app
            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient<TimeService>();  // Register HttpClient for TimeService
                    services.AddTransient<TemplateService>();
                    services.AddTransient<ExampleService>();
                })
                .Build();

            // Resolve the TimeService and run the method
            //var timeService = host.Services.GetRequiredService<TimeService>();
            //timeService.GetTimeAsync().Wait();  // Make the REST call and log the response

            var exampleService = host.Services.GetRequiredService<ExampleService>();
            exampleService.Hello();  // Log the message from appsettings.json
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while starting the application");
        }
        finally
        {
            Log.CloseAndFlush(); // Ensure that all logs are flushed before exit
        }
    }
}
