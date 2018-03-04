using System;

namespace Rhyous.StringLibrary
{
    public static class StringComparisonExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
