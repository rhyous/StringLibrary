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
        /// Checks if a string is wrapped.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="wraps">The wrap characters to check. An ArgumentNullException is thrown if this is null.</param>
        /// <returns>bool</returns>
        /// <remarks>Wrapped assumes both sides of the wrap are the same. This is not wrapped: #value!. This is wrapped: !value!. This is wrapped. #value#.</remarks>
        public static bool IsWrapped(this string value, params string[] wraps)
        {
            if (wraps == null || !wraps.Any())
                throw new ArgumentNullException("wrapChars");
            foreach (var wrapString in wraps)
            {
                if (value.StartsWith(wrapString) && value.EndsWith(wrapString))
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

        /// <summary>
        /// Unwrapse a string. Removes a wrapping string from both front and back.
        /// </summary>
        /// <param name="value">The string, possibly with quotes.</param>
        /// <param name="wrap">The string that is at both the start and end that should be removed.</param>
        /// <returns>A string without quotes.</returns>
        /// <remarks>Should not remove a matching wrapping string unless it is both at the start and the end.</remarks>
        public static string Unwrap(this string value, string wrap)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            if (value.Length >= wrap.Length * 2 && value.StartsWith(value) && value.EndsWith(value))
                value = value.Substring(wrap.Length, value.Length - wrap.Length * 2);
            return value;
        }

        /// <summary>
        /// Unwrapse a string. Removes a wrapping string from both front and back.
        /// </summary>
        /// <param name="value">The string, possibly with quotes.</param>
        /// <param name="prefix">The string that is at the start that should be removed.</param>
        /// <param name="postfix">The string that is at the end that should be removed.</param>
        /// <returns>A string without quotes.</returns>
        /// <remarks>Should not remove a matching wrapping string unless it is both at the start and the end.</remarks>
        public static string Unwrap(this string value, string prefix, string postfix)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            if (value.Length >= postfix.Length + prefix.Length &&  value.StartsWith(prefix) && value.EndsWith(postfix))
                value = value.Substring(prefix.Length, value.Length - postfix.Length - prefix.Length);
            return value;
        }
    }
}