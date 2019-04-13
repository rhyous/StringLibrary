using System;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extension method class that adds comparison opperations to string.
    /// </summary>
    public static class StringComparisonExtensions
    {
        /// <summary>
        /// This method allows you to check if a string contains another string
        /// but also pass in a StringComparison so you can choose to do this
        /// by ignoring case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="toCheck">The string to use to check if the source contains it.</param>
        /// <param name="comp">The string comparison.</param>
        /// <returns>True if the source contains the string to check, false otherwise.</returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
