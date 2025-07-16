using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rhyous.StringLibrary
{
    /// <summary>This class provides extension methods for converting a comma-separated string into a list of a specified type.</summary>
    public static class PrimitiveCollectionStringExtensions
    {
        /// <summary>Converts a comma-separated string to a list of the specified type.</summary>
        /// <typeparam name="T">The numerical type.</typeparam>
        /// <param name="s">The string</param>
        /// <param name="valueSeparator">The string separating each numeric value. The default is "," (a comma).</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>. This is needed because the decimal separator for numerics is different in different cultures.</param>
        /// <returns>A list.</returns>
        public static List<T> ToTypedList<T>(this string s, string valueSeparator = ",", CultureInfo cultureInfo = null)
            => ToTypedEnumerable<T>(s, valueSeparator, cultureInfo).ToList();

        /// <summary>Converts a comma-separated string to an array of the specified type.</summary>
        /// <typeparam name="T">The numerical type.</typeparam>
        /// <param name="s">The string</param>
        /// <param name="valueSeparator">The string separating each numeric value. The default is "," (a comma).</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>. This is needed because the decimal separator for numerics is different in different cultures.</param>
        /// <returns>A list.</returns>
        public static T[] ToTypedArray<T>(this string s, string valueSeparator = ",", CultureInfo cultureInfo = null)
            => ToTypedEnumerable<T>(s, valueSeparator, cultureInfo).ToArray();

        /// <summary>Converts a comma-separated string to an array of the specified type.</summary>
        /// <typeparam name="T">The numerical type.</typeparam>
        /// <param name="s">The string</param>
        /// <param name="valueSeparator">The string separating each numeric value. The default is "," (a comma).</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/>. This is needed because the decimal separator for numerics is different in different cultures.</param>
        /// <returns>A list.</returns>
        public static IEnumerable<T> ToTypedEnumerable<T>(this string s, string valueSeparator = ",", CultureInfo cultureInfo = null)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new List<T>();
            var decimalSeparator = cultureInfo?.NumberFormat.NumberDecimalSeparator
                                ?? CultureInfo.CurrentCulture?.NumberFormat.NumberDecimalSeparator
                                ?? CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
            if (TypeValidators.TryGetValue(typeof(T), out var validator))
            {
                return s.Split(new[] { valueSeparator }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(item => item.Trim()) // Trim whitespace
                        .Where(item => validator(item, decimalSeparator)) // only 1 decimal separator allowed
                        .Select(item => item.To<T>(default, cultureInfo));
            }
            return s.Split(new[] { valueSeparator }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(item => item.To<T>(default, cultureInfo));
        }

        /// <summary>A dictionary of type validators for primitive types.</summary>
        public static Dictionary<Type, Func<string, string, bool>> TypeValidators = new Dictionary<Type, Func<string, string, bool>>
        {
            { typeof(byte), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(sbyte), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(int), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(uint), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(long), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(ulong), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(short), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(ushort), (item, decimalSeparator) => IsValidNumericWithoutDecimal<int>(item, decimalSeparator) },
            { typeof(double), (item, decimalSeparator) => IsValidNumericWithDecimal<int>(item, decimalSeparator) },
            { typeof(decimal), (item, decimalSeparator) => IsValidNumericWithDecimal<int>(item, decimalSeparator) },
            { typeof(float), (item, decimalSeparator) => IsValidNumericWithDecimal<int>(item, decimalSeparator) }
        };

        private static bool IsValidNumericWithoutDecimal<T>(string item, string decimalSeparator)
        {
            return !string.IsNullOrWhiteSpace(item)  // Ignore empty strings
                && item.All(c => char.IsDigit(c));
        }

        private static bool IsValidNumericWithDecimal<T>(string item, string decimalSeparator)
        {
            return !string.IsNullOrWhiteSpace(item)  // Ignore empty strings
                && item.All(c => char.IsDigit(c) || decimalSeparator.Any(ds => ds == c)) // only digits and decimal separator allowed
                && new int[] { 0, 1 }.Contains(Regex.Matches(item, Regex.Escape(decimalSeparator)).Count);

        }
    }
}