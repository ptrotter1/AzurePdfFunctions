using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AzurePdfFunctions.Utils;

public class DisposingFileStreamResult : FileStreamResult
{
    public DisposingFileStreamResult(Stream fileStream, string contentType)
        : base(fileStream, contentType)
    {
    }

    public DisposingFileStreamResult(Stream fileStream, MediaTypeHeaderValue contentType)
        : base(fileStream, contentType)
    {
    }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
        await base.ExecuteResultAsync(context);
        await FileStream.DisposeAsync();
    }
}