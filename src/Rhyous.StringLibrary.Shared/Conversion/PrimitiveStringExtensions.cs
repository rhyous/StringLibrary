using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// A class that adds conversion method to simply convesion from string to primitives 
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
        /// <returns>A conversion to type T.</returns>
        public static T To<T>(this string s, T defaultValue = default(T))
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try { return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, s); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// This method override specifically detects string to string, so when they type isn't
        /// known at runtime, this method might be chosen when no conversion is necessary.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails. This parameter
        /// will never be used as it is only here to match the method signature of other calls
        /// that aren't for string.</param>
        /// <returns>The string.</returns>
        public static string To(this string s, string defaultValue = null)
        {
            return s;
        }

        /// <summary>
        /// This converts a string to the provided primitive type.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="type">The type to convert the string to.</param>
        /// <param name="defaultValue">The value to return if the converstion fails.</param>
        /// <returns>A string converted to the type or the default value.</returns>
        /// <remarks>While it is intended to be used with primitives, if another object has a
        /// TypeConverter it may still work.</remarks>
        public static object ToType(this string s, Type type, object defaultValue = null)
        {
            if (type == typeof(string))
                return s;
            if (type.IsEnum)
                return ToEnum(s, type, defaultValue);
            var isNullableT = type.IsGenericType && type.IsSubclassOf(typeof(Nullable<>));
            return ToPrimitiveOrNullable(s, type, ref defaultValue, isNullableT);
        }

        private static object ToEnum(string s, Type type, object defaulValue = null)
        {
            var mi = typeof(StringEnumExtensions).GetMethods().Single(m => m.Name == "ToEnum" && m.GetParameters().Length == 4);
            var method = mi.MakeGenericMethod(type);
            var defaultEnum = defaulValue ?? Enum.GetValues(type).GetValue(0);
            return method.Invoke(null, new object[] { s, defaultEnum, true, true });
        }

        private static object ToPrimitiveOrNullable(string s, Type type, ref object defaultValue, bool isGeneric)
        {
            var mi = typeof(PrimitiveStringExtensions).GetMethods().Single(m => m.Name == "To" && m.IsGenericMethod && m.ReturnParameter.ParameterType.IsGenericType == isGeneric);
            var method = mi.MakeGenericMethod(type);
            // Check if it is DateTime to handle this critical .NET bug
            // https://connect.microsoft.com/VisualStudio/feedback/details/733995/datetime-default-parameter-value-throws-formatexception-at-runtime
            if (type == typeof(DateTime))
                defaultValue = DateTime.MinValue;
            return method.Invoke(null, new object[] { s, defaultValue ?? Type.Missing });
        }

        /// <summary>
        /// Converts a string to a byte.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a byte.</returns>
        public static byte ToByte(this string s, byte defaultValue = 0)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a bool.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a bool.</returns>
        public static bool ToBool(this string s, bool defaultValue = false)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a DateTime.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a DateTime.</returns>
        public static DateTime ToDate(this string s, DateTime defaultValue = default(DateTime))
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a decimal.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a decimal.</returns>
        public static decimal ToDecimal(this string s, decimal defaultValue = 0.0M)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a double.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a double.</returns>
        public static double ToDouble(this string s, double defaultValue = 0.0D)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a float.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a float.</returns>
        public static float ToFloat(this string s, float defaultValue = 0.0F)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to an int.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to an int.</returns>
        public static int ToInt(this string s, int defaultValue = 0)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a long.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a long.</returns>
        public static long ToLong(this string s, long defaultValue = 0L)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to an sbyte.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to an sbyte.</returns>
        public static sbyte ToSByte(this string s, sbyte defaultValue = 0)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a short.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a short.</returns>
        public static short ToShort(this string s, short defaultValue = 0)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a uint.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a uint.</returns>
        public static uint ToUint(this string s, uint defaultValue = 0U)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a ulong.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a ulong.</returns>
        public static ulong ToULong(this string s, ulong defaultValue = 0UL)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a ushort.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a ushort.</returns>
        public static ushort ToUShort(this string s, ushort defaultValue = 0)
        {
            return To(s, defaultValue);
        }
        /// <summary>
        /// Converts a string to a guid.
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="defaultValue">The default value if convesion fails.</param>
        /// <returns>The string converted to a guid.</returns>
        public static Guid ToGuid(this string s, Guid defaultValue = default(Guid))
        {
            return To(s, defaultValue);
        }
    }
}
