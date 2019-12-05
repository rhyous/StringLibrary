using System;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for PropertyInfo to help with trimming string properties of any object
    /// </summary>
    public static class TrimPropertyInfoExtensions
    {
        /// <summary>
        /// A method that determines if a property is trimmable.
        /// </summary>
        /// <param name="pi">The PropertyInfo.</param>
        /// <returns></returns>
        public static bool IsTrimmable(this PropertyInfo pi)
        {
            if (pi == null)
                return false;
            var indexParams = pi.GetIndexParameters();
            return (indexParams == null || indexParams.Length == 0)
                && pi.PropertyType.IsTrimmable()
                && (( TrimPropertiesSettings.TrimByDefault && !Attribute.IsDefined(pi, typeof(IgnoreTrimAttribute)))
                   || !TrimPropertiesSettings.TrimByDefault && Attribute.IsDefined(pi, typeof(TrimAttribute)));
        }
    }
}
