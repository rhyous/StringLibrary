using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extensions class that provides quoting and unquoting string extensions.
    /// </summary>
    public static class QuoteStringExtensions
    {
        /// <summary>
        /// Unquotes a string. Removes both single quotes and double quote pairs.
        /// </summary>
        /// <param name="value">The string, possibly with quotes.</param>
        /// <param name="levels">The number of leves to remove.</param>
        /// <returns>A string without quotes.</returns>
        /// <remarks>Should not remove a single quote, apostrophe, that is part of the wrapped
        /// string, even if it is the start or end character.</remarks>
        public static string Unquote(this string value, int levels = 0)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            if (levels < 1)
                levels = int.MaxValue;
            while (value.Length > 1 && value.IsQuoted() && levels-- > 0)
                value = value.Substring(1, value.Length - 2);
            return value;
        }

        /// <summary>
        /// Quotes a string.
        /// </summary>
        /// <param name="value">The string to quote.</param>
        /// <param name="quote">The quote character. Default is a double quote.</param>
        /// <returns></returns>
        /// <remarks>Does not quote an already quoted string.</remarks>
        public static string Quote(this string value, char quote = '"')
        {
            if (string.IsNullOrEmpty(value))
                return value;
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
            if (string.IsNullOrWhiteSpace(value))
                return false;
            if (quotes == null || !quotes.Any())
                quotes = new[] { "'", "\"" };
            return value.IsWrapped(quotes);
        }
        
        /// <summary>
        /// Escapes quotes in a string.
        /// </summary>
        /// <param name="value">The string with quotes.</param>
        /// <param name="escapeChar"></param>
        /// <remarks>Does not escape outermost wrapping quotes.</remarks>
        public static string EscapeQuotes(this string value, char escapeChar)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            char? quoteChar = null;
            if (value.IsQuoted())
            {
                quoteChar = value[0];
                value = value.Unquote();
            }
            value = value.Replace("\"", escapeChar + "\"");
            value = value.Replace("'", escapeChar + "'");
            if (quoteChar != null)
                value = value.Quote(quoteChar.Value);
            return value;
        }

        /// <summary>
        /// Unescapes quotes, assuming that quotes are escaped by using two quote characters.
        /// </summary>
        /// <param name="value">The string with escaped quotes.</param>
        /// <returns></returns>
        public static string UnescapeQuotes(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            value = value.Replace("''", "'");
            value = value.Replace("\"\"", "\"");
            return value;
        }

        /// <summary>
        /// Unescapes quotes that are escaped with the specified escape character.
        /// </summary>
        /// <param name="value">The string with escaped quotes.</param>
        /// <param name="quoteChar">The character used to quote.</param>
        /// <param name="escapeChar">The escape character.</param>
        /// <returns></returns>
        public static string UnescapeQuotes(this string value, char quoteChar, char escapeChar)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            value = value.Replace(escapeChar.ToString() + quoteChar.ToString(), quoteChar.ToString());
            return value;
        }
    }
}
