using System;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    public static class PropertyInfoExtensions
    {
        public static bool IsTrimmable(this PropertyInfo pi)
        {
            if (pi == null)
                return false;
            var indexParams = pi.GetIndexParameters();
            return (indexParams == null || indexParams.Length == 0)
                && pi.PropertyType.IsTrimmable()
                && (( TrimPropertiesSettings.TrimByDefault && !Attribute.IsDefined(pi, typeof(IgnoreTrim)))
                   || !TrimPropertiesSettings.TrimByDefault && Attribute.IsDefined(pi, typeof(Trim)));
        }
    }
}
