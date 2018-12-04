using System;
using System.Runtime.Serialization;

namespace PS.MAD.D4AS.UseCases.Exceptions
{
    public class ImageNotValidException : Exception
    {
        public ImageNotValidException()
        {
        }

        public ImageNotValidException(string message) : base(message)
        {
        }

        public ImageNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImageNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
