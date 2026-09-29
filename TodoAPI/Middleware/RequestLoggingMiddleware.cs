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
			//request logging 
			context.Request.EnableBuffering();
			var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
			context.Request.Body.Position = 0;
			
			_logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path} \nBody: {requestBody}");

			//capture response
			var originalBodyStream = context.Response.Body;
			using var responseBody = new MemoryStream();
			context.Response.Body = responseBody;

			await _next(context); 

			context.Response.Body.Seek(0, SeekOrigin.Begin);
			var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
			context.Response.Body.Seek(0, SeekOrigin.Begin);

			_logger.LogInformation($"Outgoing Response: {context.Response.StatusCode} \nBody: {responseText}");

			await responseBody.CopyToAsync(originalBodyStream);
		}
	}
}
