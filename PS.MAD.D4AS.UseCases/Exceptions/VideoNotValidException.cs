using System;
using System.Runtime.Serialization;

namespace PS.MAD.D4AS.UseCases.Exceptions
{
    public class VideoNotValidException : Exception
    {
        public VideoNotValidException()
        {
        }

        public VideoNotValidException(string message) : base(message)
        {
        }

        public VideoNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VideoNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
