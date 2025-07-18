using SimpleMarket.Api.Exceptions;
using System.Formats.Asn1;
using System.Text.Json;

namespace SimpleMarket.Api.Middlewares
{
    public class ExceptionHandlerMiddleware :Exception
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException notFound:
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync($"{DateTime.Now}" + notFound.ToString());
                        break;
                    case ServerErrorException server:
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync($"{DateTime.Now}" +  server.ToString());
                        break;

                    default:
                        context.Response.StatusCode = 505;
                        await context.Response.WriteAsync($"{DateTime.Now}" + "Nimadur xato bor "+ ex.ToString());
                        break;
                }
            } 
        }
       
        
    }
}
