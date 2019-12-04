using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// A class that adds conversion method to simply conversion from string to primitives 
    /// and from primitives to string.
    /// </summary>
    public static class PrimitiveStringExtensions
    {
        /// <summary>
        /// This converts one type to another type. It only works with types that
        /// have a type a type converter.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="s">The string to convert.</param>
        /// <param name="defaultValue">The default value is conversion fails.</param>
        /// <param name="cultureInfo">Allows the conversion to take into account culter.</param>
        /// <returns>A conversion to type T.</returns>
        public static T To<T>(this string s, T defaultValue = default(T), CultureInfo cultureInfo = null)
        {
            cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
            if (CustomConverterTypes.Contains(typeof(T)) && s.Count(c => c == cultureInfo.NumberFormat.NumberDecimalSeparator[0]) == 1)
                return (T)Convert.ChangeType(ToLong(s, 0L, cultureInfo), typeof(T)); // First convert to long
            if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(ToBool(s, false, cultureInfo), typeof(T));
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                return (T)converter.ConvertFromString(null, cultureInfo, s);
            }
            catch
            {
                return defaultValue;
            }
        }



        private static HashSet<Type> CustomConverterTypes = new HashSet<Type>
        {
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong)
        };


        #region long16, long32, long64 overloads
        /// <summary>
        /// This overload specifically handles long. TypeConverter cannot convert a string
        /// that is an double, such as "1.0" to an long. So we first convert it to a double
        /// then we cast to an long.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="defaultValue">The default value is conversion fails.</param>
        /// <param name="cultureInfo">The culture of a string. This could matter as decimal could be a different character.</param>
        /// <returns>A conversion to type T.</returns>
        public static long To(this string s, long defaultValue = 0, CultureInfo cultureInfo = null)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(long));
            try
            {
                cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
                string separator = cultureInfo.NumberFormat.NumberDecimalSeparator;
                if (s.Count(c => c == separator[0]) == 1)
                {
                    var d = s.To<double>();
                    return Convert.ToInt64(d);
                }
                return (long)converter.ConvertFromString(null, CultureInfo.InvariantCulture, s);
            }
            catch
            {
                return defaultValue;
            }
        }
        #endregion

        /// <summary>
        /// This method override specifically detects string to string, so when they type isn't
        /// known at runtime, this method might be chosen when no conversion is necessary.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails. This parameter
        /// will never be used as it is only here to match the method signature of other calls
        /// that aren't for string.</param>
        /// <param name="cultureInfo">This is ignored in this overload, but this cannot be a proper
        /// overload if parameters don't match  exactly.</param>
        /// <returns>The string.</returns>
        public static string To(this string s, string defaultValue = null, CultureInfo cultureInfo = null)
        {
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static bool To(this string s, bool defaultValue = false, CultureInfo cultureInfo = null)
        {
            if (s.All(c => char.IsDigit(c)))
                return s.ToLong() > 0;
            return s.To<bool>();
        }

        /// <summary>
        /// This converts a string to the provided primitive type.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="type">The type to convert the string to.</param>
        /// <param name="defaultValue">The value to return if the converstion fails.</param>
        /// <param name="cultureInfo">The culture when used for string conversion.</param>
        /// <returns>A string converted to the type or the default value.</returns>
        /// <remarks>While it is intended to be used with primitives, if another object has a
        /// TypeConverter it may still work.</remarks>
        public static object ToType(this string s, Type type, object defaultValue = null, CultureInfo cultureInfo = null)
        {
            cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
            if (type == typeof(string))
                return s;
            if (type.IsEnum)
                return ToEnum(s, type, defaultValue);
            var isNullableT = type.IsGenericType && type.IsSubclassOf(typeof(Nullable<>));
            return ToPrimitiveOrNullable(s, type, ref defaultValue, isNullableT, cultureInfo);
        }

        private static object ToEnum(string s, Type type, object defaulValue = null)
        {
            var mi = typeof(StringEnumExtensions).GetMethods().Single(m => m.Name == "ToEnum" && m.GetParameters().Length == 4);
            var method = mi.MakeGenericMethod(type);
            var defaultEnum = defaulValue ?? Enum.GetValues(type).GetValue(0);
            return method.Invoke(null, new object[] { s, defaultEnum, true, true });
        }

        private static object ToPrimitiveOrNullable(string s, Type type, ref object defaultValue, bool isGeneric, CultureInfo cultureInfo = null)
        {
            cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
            var mi = typeof(PrimitiveStringExtensions).GetMethods().Single(m => m.Name == "To" && m.IsGenericMethod && m.ReturnParameter.ParameterType.IsGenericType == isGeneric);
            var method = mi.MakeGenericMethod(type);
            // Check if it is DateTime to handle this critical .NET bug
            // https://connect.microsoft.com/VisualStudio/feedback/details/733995/datetime-default-parameter-value-throws-formatexception-at-runtime
            if (type == typeof(DateTime))
                defaultValue = DateTime.MinValue;
            return method.Invoke(null, new object[] { s, defaultValue ?? Type.Missing, cultureInfo });
        }

        /// <summary>
        /// Converts a string to a byte.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a byte.</returns>
        public static byte ToByte(this string s, byte defaultValue = 0, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a bool.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a bool.</returns>
        public static bool ToBool(this string s, bool defaultValue = false, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a DateTime.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a DateTime.</returns>
        public static DateTime ToDate(this string s, DateTime defaultValue = default(DateTime), CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a decimal.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a decimal.</returns>
        public static decimal ToDecimal(this string s, decimal defaultValue = 0.0M, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a double.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a double.</returns>
        public static double ToDouble(this string s, double defaultValue = 0.0D, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a float.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a float.</returns>
        public static float ToFloat(this string s, float defaultValue = 0.0F, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to an int.</returns>
        public static int ToInt(this string s, int defaultValue = 0, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a long.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a long.</returns>
        public static long ToLong(this string s, long defaultValue = 0L, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to an sbyte.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to an sbyte.</returns>
        public static sbyte ToSByte(this string s, sbyte defaultValue = 0, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a short.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a short.</returns>
        public static short ToShort(this string s, short defaultValue = 0, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a uint.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a uint.</returns>
        public static uint ToUint(this string s, uint defaultValue = 0U, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a ulong.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a ulong.</returns>
        public static ulong ToULong(this string s, ulong defaultValue = 0UL, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a ushort.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a ushort.</returns>
        public static ushort ToUShort(this string s, ushort defaultValue = 0, CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
        /// <summary>
        /// Converts a string to a guid.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <param name="cultureInfo">The culture.</param>
        /// <returns>The string converted to a guid.</returns>
        public static Guid ToGuid(this string s, Guid defaultValue = default(Guid), CultureInfo cultureInfo = null)
        {
            return To(s, defaultValue, cultureInfo);
        }
    }
}
