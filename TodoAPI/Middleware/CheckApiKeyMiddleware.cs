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
			var path = context.Request.Path;
			if (path.StartsWithSegments("/scalar") || path.StartsWithSegments("/swagger"))
			{
				await _next(context);
				return;
			}

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
