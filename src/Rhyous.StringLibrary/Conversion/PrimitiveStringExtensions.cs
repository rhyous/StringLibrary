using System;

namespace Rhyous.StringLibrary
{
    public static class PrimitiveStringExtensions
    {
        internal delegate bool TryParseMethod<T>(string s, out T result);

        internal static T TryParse<T>(string value, TryParseMethod<T> handler, T defaultValue)
            where T : struct, IComparable, IConvertible, IComparable<T>, IEquatable<T>
        {
            value = value.TrimAll();
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            T result;
            if (handler(value, out result))
                return result;
            return defaultValue;
        }

        public static byte ToByte(this string s, byte defaultValue = 0)
        {
            return TryParse(s, byte.TryParse, defaultValue);
        }

        public static bool ToBool(this string s, bool defaultValue = false)
        {
            return TryParse(s, bool.TryParse, defaultValue);
        }

        public static DateTime ToDate(this string s, DateTime defaultValue = default(DateTime))
        {
            return TryParse(s, DateTime.TryParse, defaultValue);
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0.0M)
        {
            return TryParse(s, decimal.TryParse, defaultValue);
        }

        public static double ToDouble(this string s, double defaultValue = 0.0D)
        {
            return TryParse(s, double.TryParse, defaultValue);
        }

        public static float ToFloat(this string s, float defaultValue = 0.0F)
        {
            return TryParse(s, float.TryParse, defaultValue);
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            return TryParse(s, int.TryParse, defaultValue);
        }

        public static long ToLong(this string s, long defaultValue = 0L)
        {
            return TryParse(s, long.TryParse, defaultValue);
        }

        public static sbyte ToSByte(this string s, sbyte defaultValue = 0)
        {
            return TryParse(s, sbyte.TryParse, defaultValue);
        }

        public static short ToShort(this string s, short defaultValue = 0)
        {
            return TryParse(s, short.TryParse, defaultValue);
        }

        public static uint ToUint(this string s, uint defaultValue = 0U)
        {
            return TryParse(s, uint.TryParse, defaultValue);
        }

        public static ulong ToULong(this string s, ulong defaultValue = 0UL)
        {
            return TryParse(s, ulong.TryParse, defaultValue);
        }

        public static ushort ToUShort(this string s, ushort defaultValue = 0)
        {
            return TryParse(s, ushort.TryParse, defaultValue);
        }
    }
}
