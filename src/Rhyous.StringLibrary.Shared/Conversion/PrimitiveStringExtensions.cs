using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Rhyous.StringLibrary
{
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

        public static string To(this string s, string defaultValue = null)
        {
            return s;
        }

        public static object ToType(this string s, Type type, object defaultValue = null)
        {
            if (type == typeof(string))
                return s;
            MethodInfo mi = null;
            MethodInfo method = null;
            if (type.IsEnum)
                return ToEnum(s, type, out mi, out method);
            var isNullableT = type.IsGenericType && type.IsSubclassOf(typeof(Nullable<>));
            return ToPrimitiveOrNullable(s, type, ref defaultValue, out mi, out method, isNullableT);
        }

        private static object ToEnum(string s, Type type, out MethodInfo mi, out MethodInfo method)
        {
            mi = typeof(StringEnumExtensions).GetMethods().Single(m => m.Name == "ToEnum" && m.GetParameters().Length == 4);
            method = mi.MakeGenericMethod(type);
            var defaultEnum = Enum.GetValues(type).GetValue(0);
            return method.Invoke(null, new object[] { s, defaultEnum, true, true });
        }

        private static object ToPrimitiveOrNullable(string s, Type type, ref object defaultValue, out MethodInfo mi, out MethodInfo method, bool isGeneric)
        {
            mi = typeof(PrimitiveStringExtensions).GetMethods().Single(m => m.Name == "To" && m.IsGenericMethod && m.ReturnParameter.ParameterType.IsGenericType == isGeneric);
            method = mi.MakeGenericMethod(type);
            // Check if it is DateTime to handle this critical .NET bug
            // https://connect.microsoft.com/VisualStudio/feedback/details/733995/datetime-default-parameter-value-throws-formatexception-at-runtime
            if (type == typeof(DateTime))
                defaultValue = DateTime.MinValue;
            return method.Invoke(null, new object[] { s, defaultValue ?? Type.Missing });
        }

        public static byte ToByte(this string s, byte defaultValue = 0)
        {
            return To(s, defaultValue);
        }

        public static bool ToBool(this string s, bool defaultValue = false)
        {
            return To(s, defaultValue);
        }

        public static DateTime ToDate(this string s, DateTime defaultValue = default(DateTime))
        {
            return To(s, defaultValue);
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0.0M)
        {
            return To(s, defaultValue);
        }

        public static double ToDouble(this string s, double defaultValue = 0.0D)
        {
            return To(s, defaultValue);
        }

        public static float ToFloat(this string s, float defaultValue = 0.0F)
        {
            return To(s, defaultValue);
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            return To(s, defaultValue);
        }

        public static long ToLong(this string s, long defaultValue = 0L)
        {
            return To(s, defaultValue);
        }

        public static sbyte ToSByte(this string s, sbyte defaultValue = 0)
        {
            return To(s, defaultValue);
        }

        public static short ToShort(this string s, short defaultValue = 0)
        {
            return To(s, defaultValue);
        }

        public static uint ToUint(this string s, uint defaultValue = 0U)
        {
            return To(s, defaultValue);
        }

        public static ulong ToULong(this string s, ulong defaultValue = 0UL)
        {
            return To(s, defaultValue);
        }

        public static ushort ToUShort(this string s, ushort defaultValue = 0)
        {
            return To(s, defaultValue);
        }

        public static Guid ToGuid(this string s, Guid defaultValue = default(Guid))
        {
            return To(s, defaultValue);
        }
    }
}
