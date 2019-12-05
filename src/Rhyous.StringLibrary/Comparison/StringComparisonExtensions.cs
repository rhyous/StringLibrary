using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extension method class that adds comparison opperations to string.
    /// </summary>
    public static class StringComparisonExtensions
    {
        #region StartsWithAny
        /// <summary>
        /// Checks if a string starts with any of the following values.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="values">The values.</param>
        /// <returns>True if the string starts with any of the provided values, false otherwise.</returns>
        public static bool StartsWithAny(this string str, params string[] values)
        {
            return values.Any(v => str.StartsWith(v));
        }

        /// <summary>
        /// Checks if a string starts with any of the following values.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="comparison">The StringComparison that Specifies the culture, case, and sort rules to be used.</param>
        /// <param name="values">The values.</param>
        /// <returns>True if the string starts with any of the provided values, false otherwise.</returns>
        public static bool StartsWithAny(this string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase, params string[] values)
        {
            return values.Any(v => str.StartsWith(v, comparison));
        }
        #endregion

        #region EndsWithAny
        /// <summary>
        /// Checks if a string ends with any of the following values.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="values">The values.</param>
        /// <returns>True if the string ends with any of the provided values, false otherwise.</returns>
        public static bool EndsWithAny(this string str, params string[] values)
        {
            return values.Any(v => str.EndsWith(v));
        }

        /// <summary>
        /// Checks if a string ends with any of the following values.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <param name="comparison">The StringComparison that Specifies the culture, case, and sort rules to be used.</param>
        /// <param name="values">The values.</param>
        /// <returns>True if the string ends with any of the provided values, false otherwise.</returns>
        public static bool EndsWithAny(this string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase, params string[] values)
        {
            return values.Any(v => str.EndsWith(v, comparison));
        }
        #endregion


        #region Contains
        /// <summary>
        /// This method allows you to check if a string contains another string
        /// but also pass in a StringComparison so you can choose to do this
        /// by ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="toCheck">The string to use to check if the source contains it.</param>
        /// <param name="comparison">The StringComparison that Specifies the culture, case, and sort rules to be used.</param>
        /// <returns>True if the source contains the string to check, false otherwise.</returns>
        public static bool Contains(this string source, string toCheck, StringComparison comparison)
        {
            return source.IndexOf(toCheck, comparison) >= 0;
        }

        /// <summary>
        /// This method allows you to check if a string contains another string
        /// but also pass in a StringComparison so you can choose to do this
        /// by ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="values">The strings to use to check if the source contains it.</param>
        /// <returns>True if the source contains the string to check, false otherwise.</returns>
        public static bool ContainsAny(this string source, params string[] values)
        {
            return values.Any(v => source.Contains(v));
        }

        /// <summary>
        /// This method allows you to check if a string contains another string
        /// but also pass in a StringComparison so you can choose to do this
        /// by ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="comparison">The StringComparison that Specifies the culture, case, and sort rules to be used.</param>
        /// <param name="values">The string to use to check if the source contains it.</param>
        /// <returns>True if the source contains the string to check, false otherwise.</returns>
        public static bool ContainsAny(this string source, StringComparison comparison, params string[] values)
        {
            return values.Any(v => source.Contains(v, comparison));
        }
        #endregion
    }
}
