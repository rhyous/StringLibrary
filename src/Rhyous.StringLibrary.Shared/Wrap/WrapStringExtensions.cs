using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for Wrapping or Unwrapping strings
    /// </summary>
    public static class WrapStringExtensions
    {
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
            if (value.IsQuoted(new[] { quote.ToString() }))
                return value;
            return value.Wrap(quote);
        }

        /// <summary>
        /// Checks if a string is quoted.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="quotes">The quote characters to check the prefix and suffix. Default is both a double quote and a single quote.</param>
        /// <returns>bool</returns>
        /// <remarks>Wrapped assumes both sides of the quote are the same. This is not quote: "value'. This is wrapped: "value". This is wrapped. 'value'.</remarks>
        public static bool IsQuoted(this string value, params string[] quotes)
        {
            if (quotes == null || !quotes.Any())
                quotes = new[] { "'", "\"" };
            return value.IsWrapped(quotes);
        }

        /// <summary>
        /// Checks if a string is quoted.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="wraps">The wrap characters to check. An ArgumentNullException is thrown if this is null.</param>
        /// <returns>bool</returns>
        /// <remarks>Wrapped assumes both sides of the wrap are the same. This is not wrapped: #value!. This is wrapped: !value!. This is wrapped. #value#.</remarks>
        public static bool IsWrapped(this string value, params string[] wraps)
        {
            if (wraps == null || !wraps.Any())
                throw new ArgumentNullException("wrapChars");
            foreach (var wrapChar in wraps)
            {
                var quoteCharAsString = wrapChar.ToString();
                if (value.StartsWith(quoteCharAsString) && value.EndsWith(quoteCharAsString))
                    return true;
            }
            return false;
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
