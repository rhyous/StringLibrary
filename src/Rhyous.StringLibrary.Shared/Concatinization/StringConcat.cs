using System;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class StringConcat
    {
        /// <summary>
        /// Concatenates two strings with a separator, and handles cases where the
        /// separator may or may not already exist, as well as trimming whitespace.
        /// </summary>
        /// <param name="separator">The separator character.</param>
        /// <param name="strings">The strings to combine.</param>
        /// <returns>The strings trimmed, and combined with 1 separator between them.</returns>
        /// <remarks>This should work similar to Path.Combine, only with the ability to choose the separator,
        /// and no path rooting when subsequent strings start with a /.</remarks>
        public static string WithSeparator(char separator, params string[] strings)
        {
            if (strings == null || strings.Length == 0)
                throw new ArgumentNullException(nameof(strings));
            string concatenatedString = null;
            for (int i = 0; i < strings.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(strings[i]))
                    throw new ArgumentNullException(nameof(strings), $"Item {i} cannot be null.");
                string current;
                do
                {
                    current = strings[i];
                    strings[i] = strings[i].Trim();
                    strings[i] = i == 0 ? strings[i].TrimEnd(separator) 
                                        : strings[i].Trim(separator);
                } while (strings[i] != current);
                concatenatedString = string.IsNullOrEmpty(concatenatedString)
                                   ? strings[i]
                                   : $"{concatenatedString}{separator}{strings[i]}";
            }
            return concatenatedString;
        }
    }
}
