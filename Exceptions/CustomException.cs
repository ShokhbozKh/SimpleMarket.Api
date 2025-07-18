namespace SimpleMarket.Api.Exceptions
{
    public class CustomException: Exception
    {
        public int StatusCode {  get; }

            // bu orqali biz throw qilingan xatolikka matn yozamiz,(server error), 500
        public CustomException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
        
    }
}
