using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Legacy.Web;

using System.Net.Http.Headers;
using ExcelService.Writers;

public class TemplateWriterResult(TemplateWriter templateWriter, string fileName, string sheetName) : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
            using var memoryStream = templateWriter.Render(sheetName);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(memoryStream.ToArray())
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };

            await response.Content.CopyToAsync(context.HttpContext.Response.Body);
        }
}

public class NPoiResult(MemoryStream memoryStream, string fileName) : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
            using (memoryStream)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                }; 
                
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                await response.Content.CopyToAsync(context.HttpContext.Response.Body);
            }
        }
}