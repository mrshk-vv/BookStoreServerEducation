using System;

namespace Store.Shared.Common
{
    public class ServerException : Exception
    {
        public Enums.Enums.Errors ErrorCode { get; set; }
        public string Description { get; set; }

        public ServerException(string description,Enums.Enums.Errors errorCode = Enums.Enums.Errors.None)
        {
            ErrorCode = errorCode;
            Description = description;
        }
    }
}
