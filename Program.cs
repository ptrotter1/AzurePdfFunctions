using System.Runtime.InteropServices;
using AzurePdfFunctions.Models;
using AzurePdfFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PuppeteerSharp;

// Configure PuppeteerSharp to download the browser binary to a temporary directory on Linux
var bfOptions = new BrowserFetcherOptions();
if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    bfOptions.Path = Path.GetTempPath();
}
var bf = new BrowserFetcher(bfOptions);

// Download the browser binary
await bf.DownloadAsync(PuppeteerSharp.BrowserData.Chrome.DefaultBuildId);

// Configure PuppeteerSharp to use the downloaded browser binary
var puppeteerConfig = new PuppeteerConfig
{
    BrowserExecutablePath = bf.GetExecutablePath(PuppeteerSharp.BrowserData.Chrome.DefaultBuildId),
};

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton(puppeteerConfig);
        services.AddScoped<HtmlToPdfService>();
    })
    .Build();

host.Run();
