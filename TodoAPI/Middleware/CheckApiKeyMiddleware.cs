namespace TodoAPI.Middleware
{
	public class CheckApiKeyMiddleware
	{
		private readonly RequestDelegate _next;

		public CheckApiKeyMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.Request.Headers.ContainsKey("X-API-KEY"))
			{
				context.Response.StatusCode = 401; 
				await context.Response.WriteAsync("API key is missing");
				return;
			}
			await _next(context);
		}
	}
}
