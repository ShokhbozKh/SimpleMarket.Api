namespace SimpleMarket.Api.Middlewares
{
    public class IpLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public IpLoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            ip = httpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine($"Your ip address: {ip}");
            

            await _next(httpContext); // So'rov ketib qaytib keladi keyin pastga utadi

            Console.WriteLine("Surov tugadi");

        }
    }
}
