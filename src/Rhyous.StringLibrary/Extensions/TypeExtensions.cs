using System;
using System.Linq;

namespace Rhyous.StringLibrary
{
    public static class TypeExtensions
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
