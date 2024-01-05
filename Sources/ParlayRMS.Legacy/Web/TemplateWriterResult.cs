using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Legacy.Web;

using ExcelService.Writers;

public class TemplateWriterResult(TemplateWriter templateWriter, string fileName) : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        
        response.ContentType ="application/vnd.ms-excel";
        response.Headers.TryAdd("Content-Disposition", new[] { "attachment; filename=" + fileName });
        using var memoryStream = templateWriter.Render();
        await memoryStream.CopyToAsync(response.Body);
    }
}

public class NPoiResult(MemoryStream memoryStream, string fileName) : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        using (memoryStream)
        {
            var response = context.HttpContext.Response;

            response.ContentType = "application/vnd.ms-excel";
            context.HttpContext.Response.Headers.TryAdd("Content-Disposition", new[] { "attachment; filename=" + fileName });
            await memoryStream.CopyToAsync(context.HttpContext.Response.Body);
        }
    }
}