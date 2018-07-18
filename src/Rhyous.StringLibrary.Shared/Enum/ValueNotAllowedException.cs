using System;
using System.Runtime.Serialization;

namespace Rhyous.StringLibrary
{
    public class ValueNotAllowedException : ArgumentException
    {
        public ValueNotAllowedException()
        {
        }

        public ValueNotAllowedException(string message) : base(message)
        {
        }

        public ValueNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValueNotAllowedException(string message, string paramName) : base(message, paramName)
        {
        }

        public ValueNotAllowedException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected ValueNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
