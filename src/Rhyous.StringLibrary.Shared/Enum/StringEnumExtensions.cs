using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An extension class that adds conversion methods from string to enum.
    /// </summary>
    public static class StringEnumExtensions
    {
        /// <summary>
        /// A method to convert a string to a nullable Enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="str">The string.</param>
        /// <param name="ignoreCase">A bool valud to ignore case or not.</param>
        /// <param name="allowNumeric">A bool value for whether to allow numeric strings.</param>
        /// <returns>A nullable enum. An enum unless conversion fails, then null.</returns>
        public static T? ToEnum<T>(this string str, bool ignoreCase = true, bool allowNumeric = true)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("", "str");
            if (string.IsNullOrWhiteSpace(str))
                return null;
            var isNumeric = str.All(c => char.IsDigit(c));
            if ((allowNumeric || !isNumeric) && Enum.TryParse(str, ignoreCase, out T ret))
                return ret;
            return null;
        }

        /// <summary>
        /// A method to convert a string to an Enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="str">The string.</param>
        /// <param name="defaultValue">The default enum value.</param>
        /// <param name="ignoreCase">A bool valud to ignore case or not.</param>
        /// <param name="allowNumeric">A bool value for whether to allow numeric strings.</param>
        /// <returns>An enum unless conversion fails, then the default value.</returns>
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
