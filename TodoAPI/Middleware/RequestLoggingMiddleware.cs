using System.Diagnostics;

namespace TodoAPI.Middleware
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<RequestLoggingMiddleware> _logger;
		public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			await _next(context);
			sw.Stop();
			_logger.LogInformation("Request {method} {url} => {statusCode} in {elapsedMilliseconds}ms",
				context.Request.Method, 
				context.Request.Path, 
				context.Response.StatusCode,
				sw.ElapsedMilliseconds);
		}
	}
}
