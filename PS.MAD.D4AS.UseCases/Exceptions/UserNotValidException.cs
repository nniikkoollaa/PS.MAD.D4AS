using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PS.MAD.D4AS.UseCases.Exceptions
{
    public class UserNotValidException : Exception
    {
        public UserNotValidException()
        {
        }

        public UserNotValidException(string message) : base(message)
        {
        }

        public UserNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
