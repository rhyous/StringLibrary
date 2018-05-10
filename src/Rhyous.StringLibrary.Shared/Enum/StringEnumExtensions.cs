using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    public static class StringEnumExtensions
    {
        public static T? ToEnum<T>(this string str, bool ignoreCase = true, bool allowNumeric = true)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("", "str");
            if (string.IsNullOrWhiteSpace(str))
                return null;
            var isNumeric = str.All(c => Char.IsDigit(c));
            if ((allowNumeric || !isNumeric) && Enum.TryParse(str, ignoreCase, out T ret))
                return ret;
            return null;
        }

        public static T ToEnum<T>(this string str, T defaultValue, bool ignoreCase = true, bool allowNumeric = true)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enum", "T");
            if (string.IsNullOrWhiteSpace(str))
                return defaultValue;
            var isNumeric = str.All(c => Char.IsDigit(c));
            if ((allowNumeric || !isNumeric) && Enum.TryParse(str, ignoreCase, out T ret))
                return ret;
            return defaultValue;
        }
    }
}
