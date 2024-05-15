using AzurePdfFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzurePdfFunctions
{
    public class HtmlToPdfStreamHttpPost
    {
        private readonly HtmlToPdfService _htmlToPdfService;
        private readonly ILogger<HtmlToPdfStreamHttpPost> _logger;

        public HtmlToPdfStreamHttpPost(HtmlToPdfService htmlToPdfService, ILogger<HtmlToPdfStreamHttpPost> logger)
        {
            _htmlToPdfService = htmlToPdfService;
            _logger = logger;
        }

        [Function("HtmlToPdfStreamHttpPost")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("HtmlToPdfStreamHttpPost triggered.");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var pdfStream = await _htmlToPdfService.GetPdf(requestBody);
            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}
