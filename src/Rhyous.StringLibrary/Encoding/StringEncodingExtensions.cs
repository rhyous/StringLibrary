using System;
using System.Text;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// And extension method to allow for easier Base64 encoding and decoding of strings.
    /// </summary>
    public static class StringEncodingExtensions
    {
        /// <summary>
        /// Base64 encodes a string.
        /// </summary>
        /// <param name="str">The plain text string to Base64 encode.</param>
        /// <param name="encoding">Default value: Encoding.Utf8</param>
        /// <returns>A Base64 encoded string.</returns>        
        public static string Base64Encode(this string str, Encoding encoding = null)
        {
            if (str == null)
                return null;
            encoding = encoding ?? Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(str));
        }

        /// <summary>
        /// Base64 decodes a string.
        /// </summary>
        /// <param name="str">The Base64 encoded string to decode.</param>
        /// <param name="encoding">Default value: Encoding.Utf8</param>
        /// <returns>A plain text string.</returns>     
        public static string Base64Decode(this string str, Encoding encoding = null)
        {
            if (str == null)
                return null;
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(Convert.FromBase64String(str));
        }
    }
}