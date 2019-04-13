using System.IO;
using System.Text;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extensions class that provides methods to string or method to stream to convert
    /// to and from each other.
    /// </summary>
    public static class StreamStringExtensions
    {
        /// <summary>
        /// Converts a string to a stream.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A stream.</returns>
        public static Stream ToStream(this string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8; // Default
            }
            return new MemoryStream(encoding.GetBytes(str ?? ""));
        }

        /// <summary>
        /// Converts a stream to a string.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A string.</returns>
        public static string AsString(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8; // Default
            }
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
