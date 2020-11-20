namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extension method for truncating strings.
    /// </summary>
    public static class StringTruncateExtensions
    {
        /// <summary>
        /// If the string is longer than maxLenght, it truncates at string at the maxLength.
        /// </summary>
        /// <param name="s">the string</param>
        /// <param name="maxLength">the max length</param>
        /// <returns></returns>
        public static string Truncate(this string s, int maxLength)
        {
            if (s == null || s.Length <= maxLength)
                return s;
            return s.Substring(0, maxLength);
        }
    }
}
