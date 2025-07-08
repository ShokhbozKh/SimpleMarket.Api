namespace SimpleMarket.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
            public LoggingMiddleware(RequestDelegate next)
            {
                _next = next ?? throw new ArgumentNullException(nameof(next));
            }
    
            public async Task InvokeAsync(HttpContext context)
            {
                // Log the request details
                Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    
                // Call the next middleware in the pipeline
                await _next(context);
    
                // Log the response details
                Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}
