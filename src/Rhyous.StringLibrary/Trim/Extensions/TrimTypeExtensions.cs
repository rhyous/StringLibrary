using System;
using System.Collections;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for Type to help with trimming string properties of any object
    /// </summary>
    public static class TrimTypeExtensions
    {
        /// <summary>
        /// A method to determine if a type is trimmable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if the type is trimmable.</returns>
        public static bool IsTrimmable(this Type type)
        {
            return !type.IsPrimitive && (!type.IsEnumerable() || type.IsSupportedCollection());
        }

        /// <summary>
        /// A method to determine if a generic type is a supported trimmable type.
        /// </summary>
        /// <param name="type">the generic type.</param>
        /// <returns>True if the type is trimmable.</returns>
        public static bool IsSupportedCollection(this Type type)
        {
            while (type != null)
            {
                var supportedTypes = TrimPropertiesSettings.SupportedCollectionTypes;
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

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null;
        }
    }
}
