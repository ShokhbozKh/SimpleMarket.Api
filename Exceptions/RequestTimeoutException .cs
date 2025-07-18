namespace SimpleMarket.Api.Middlewares
{
    public class RequestTimeoutException : Exception
    {
        public RequestTimeoutException(string message) 
            : base(message)
        {
        }
    }
}
        

        
