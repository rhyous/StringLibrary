using System;
using System.Diagnostics.CodeAnalysis;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An exception class thrown by the AllowedString object when an attempt is made 
    /// to set an AllowedString to a value that is not allowed.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValueNotAllowedException : ArgumentException
    {
        /// <summary>
        /// The constructor for ValueNotAllowedException.
        /// </summary>
        /// <param name="message">The error.</param>
        /// <param name="paramName">The parameter.</param>
        public ValueNotAllowedException(string message, string paramName) 
            : base(message, paramName)
        {
        }
    }
}
