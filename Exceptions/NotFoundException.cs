using Microsoft.IdentityModel.Tokens;

namespace SimpleMarket.Api.Exceptions
{
    public class NotFoundException :Exception
    {
        public NotFoundException(string message): 
            base(message)
        {

        }
    }
}
