using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for Type to help with trimming string properties of any object
    /// </summary>
    public static class TrimTypeExtensions
    {
        public static bool IsTrimmable(this Type type)
        {
            return !type.IsPrimitive && (!type.IsGenericType || type.IsSupportedGeneric());
        }
        
        public static bool IsSupportedGeneric(this Type type)
        {
            while (type != null)
            {
                var supportedTypes = TrimPropertiesSettings.SupportedGenericTypes;
                if (supportedTypes.Contains(type)
                    || (type.IsGenericType && (supportedTypes.Contains(type.GetGenericTypeDefinition())))
                    || type.GetInterfaces().Any(i => supportedTypes.Contains(i)))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
