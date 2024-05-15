using AzurePdfFunctions.Models;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;

namespace AzurePdfFunctions.Services;

public class HtmlToPdfService
{
    private readonly PuppeteerConfig _puppeteerConfig;
    private readonly ILogger<HtmlToPdfService> _logger;

    public HtmlToPdfService(PuppeteerConfig puppeteerConfig, ILogger<HtmlToPdfService> logger)
    {
        _puppeteerConfig = puppeteerConfig;
        _logger = logger;
    }

    public async Task<Stream> GetPdf(string htmlString)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(htmlString);
        var base64Encoded = Convert.ToBase64String(plainTextBytes);
        
        _logger.LogInformation("Launching browser...");
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = _puppeteerConfig.BrowserExecutablePath,
            Args = ["--no-sandbox"]
        });
        
        _logger.LogInformation("Opening new tab...");
        await using var page = await browser.NewPageAsync();
        
        _logger.LogInformation("Navigating to data URL...");
        await page.GoToAsync("data:text/html;base64," + base64Encoded);

        _logger.LogInformation("Generating PDF...");
        var stream = await page.PdfStreamAsync();

        return stream;
    }
}