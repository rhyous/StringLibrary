using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            encoding = encoding ?? Encoding.UTF8; // Default
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
            encoding = encoding ?? Encoding.UTF8; // Default
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Converts a stream to a string, asynchronously.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A Task{string}. Use await or call task.Result to get the string.</returns>
        public static async Task<string> AsStringAsync(this Stream stream, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8; // Default
            
            using (var reader = new StreamReader(stream, encoding))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}