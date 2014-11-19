using System;
using System.Runtime.Serialization;

namespace uFluent
{
    public class FluentException : Exception
    {
        public FluentException()
        {
        }

        public FluentException(string message) : base(message)
        {
        }

        public FluentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FluentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
