using System.Text;

namespace Rhyous.StringLibrary
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes whitespace from the front and back of a string and also removes any
        /// extra whitespace in the middle of a string and/or replaces any whitespace
        /// in the middle of a string with a single space.
        /// </summary>
        /// <param name="value">The string</param>
        /// <returns>A trimmed string.</returns>
        public static string TrimAll(this string value)
        {
            var trimmedValue = new StringBuilder();
            char previousChar = (char)0;
            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    previousChar = c;
                    continue;
                }
                if (char.IsWhiteSpace(previousChar) && trimmedValue.Length > 0)
                {
                    trimmedValue.Append(' ');
                }
                trimmedValue.Append(c);
                previousChar = c;
            }
            return trimmedValue.ToString();
        }

        /// <summary>
        /// Unquotes a string. Removes both single quotes and double quotes.
        /// </summary>
        /// <param name="value">The string, possibly with quotes.</param>
        /// <returns>A string without quotes.</returns>
        public static string Unquote(this string value)
        {
            return value.Trim(new[] { '"', '\'' });
        }

        /// <summary>
        /// Quotes a string.
        /// </summary>
        /// <param name="value">The string to quote.</param>
        /// <param name="quote">The quote character. Default is a double quote.</param>
        /// <returns></returns>
        public static string Quote(this string value, char quote = '"')
        {
            return value.Wrap(quote);
        }

        /// <summary>
        /// Wraps a string.
        /// </summary>
        /// <param name="value">The value string.</param>
        /// <param name="surround">The surround string to put in front and back of the value string.</param>
        /// <returns>A string wrapped in front and back with the value of surround.</returns>
        public static string Wrap(this string value, string surround)
        {
            return value.Wrap(surround, surround);
        }

        /// <summary>
        /// Wraps a string in a prefix and postfix.
        /// </summary>
        /// <param name="value">The value string.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="postfix">The postfix.</param>
        /// <returns>A string wrapped in a prefix and postfix.</returns>
        public static string Wrap(this string value, string prefix, string postfix)
        {
            return $"{prefix}{value}{postfix}";
        }

        /// <summary>
        /// Wraps a string.
        /// </summary>
        /// <param name="value">The value string.</param>
        /// <param name="surround">The surround character to put in front and back of the value string.</param>
        /// <returns>A string wrapped in front and back with the value of surround.</returns>
        public static string Wrap(this string value, char surround)
        {
            return value.Wrap(surround, surround);
        }

        /// <summary>
        /// Wraps a string in a prefix and postfix.
        /// </summary>
        /// <param name="value">The value string.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="postfix">The postfix.</param>
        /// <returns>A string wrapped in a prefix and postfix.</returns>
        public static string Wrap(this string value, char prefix, char postfix)
        {
            return $"{prefix}{value}{postfix}";
        }
    }
}