namespace SimpleMarket.Api.Middlewares
{
    public class HelloMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public HelloMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Hello  this is Middleware starting");
            await _requestDelegate(context);
            Console.WriteLine("Hello  middleware ending...");
        }
    }
}
