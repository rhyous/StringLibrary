using System.Text;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for trimming strings
    /// </summary>
    public static class TrimStringExtensions
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
    }
}