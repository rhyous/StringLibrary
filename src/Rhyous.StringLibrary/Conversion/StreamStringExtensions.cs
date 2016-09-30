using System.IO;
using System.Text;

namespace Rhyous.StringLibrary
{
    public static class StreamStringExtensions
    {
        public static Stream ToStream(this string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8; // Default
            }
            return new MemoryStream(encoding.GetBytes(str ?? ""));
        }

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
