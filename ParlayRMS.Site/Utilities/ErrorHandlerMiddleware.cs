namespace ParlayRMS.Site.Utilities;

using Fanex.Logging;
using Fanex.Logging.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception exception)
		{
			await HandleException(context, exception);
		}
	}

	private async Task HandleException(HttpContext context, Exception exception)
	{
		var logMessage = new LogMessage(exception.Message)
            .AddSection("Server Info", CommonInfoGetters.GetServerInfo())
			.AddSection("Exception Info", CommonInfoGetters.GetExceptionInfo(exception))
			.ToString();
		await logger.ErrorAsync(logMessage);

		var method = context.Request.Method.ToLowerInvariant();
		var requestedWith = context.Request.Headers["X-Requested-With"];

		if (method is "post" or "put" or "delete" && requestedWith == "XMLHttpRequest")
		{
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		}

		await context.Response.WriteAsync(exception.Message);
		await context.Response.WriteAsync("<br/>");
		await context.Response.WriteAsync(exception.StackTrace);
	}
}