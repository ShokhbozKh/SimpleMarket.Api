﻿namespace SimpleMarket.Api.Exceptions
{
    public class ServerErrorException: Exception
    {
        public ServerErrorException(string message):
            base(message)
        {
            
        }
    }
}
