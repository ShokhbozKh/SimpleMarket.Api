using SimpleMarket.Api.Exceptions;

namespace SimpleMarket.Api.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly int _maxSecund;
        private readonly RequestDelegate _requestDelegate;

        public RequestTimingMiddleware(RequestDelegate requestDelegate, int maxSecund=300)
        {
            _maxSecund = maxSecund;
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                await _requestDelegate(httpContext);
                stopwatch.Stop();

                var elapsedMs = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"[{DateTime.Now}] So‘rov vaqti: {elapsedMs} ms | {httpContext.Request.Method} {httpContext.Request.Path}");

                if (elapsedMs > _maxSecund)
                {
                    // Vaqt me’yordan oshdi — xatolik chiqaramiz
                    throw new RequestTimeoutException($"So‘rov ({httpContext.Request.Method} {httpContext.Request.Path}) {elapsedMs} msda tugadi, lekin ruxsat etilganidan oshib ketdi ({_maxSecund} ms).");
                }
           
           
        }
    }
}
