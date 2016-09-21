using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhyous.StringLibrary
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Finds any property of type string and trims it, unless that property has the
        /// IgnoreTrimAttribute applied.
        /// </summary>
        /// <param name="obj"></param>
        public static void TrimStringProperties(this object obj)
        {
            var stringProperties = obj.GetType().GetProperties()
                          .Where(p => p.PropertyType == typeof(string)
                              && !Attribute.IsDefined(p, typeof(IgnoreTrim)));

            foreach (var stringProperty in stringProperties)
            {
                var currentValue = (string)stringProperty.GetValue(obj, null);
                if (currentValue != null)
                    stringProperty.SetValue(obj, currentValue.TrimAll(), null);
            }

            if (IsGenericList(obj))
            {
                var enumerable = obj as IEnumerable;
                if (enumerable != null)
                {
                    foreach (var item in enumerable)
                    {
                        item.TrimStringProperties();
                    }
                }
                return;
            }

            var subClasses = obj.GetType().GetProperties()
                .Where(p => p.PropertyType.IsClass
                            && p.PropertyType != typeof(string)
                            && !Attribute.IsDefined(p, typeof(IgnoreTrim)))
                            .Select(p => p.GetValue(obj, null)).Where(c => c != null);
            foreach (var subClass in subClasses)
            {
                if (subClass.GetType().IsPrimitive || subClass.GetType().IsArray || (subClass.GetType().IsGenericType && subClass.GetType().GetGenericTypeDefinition() == typeof(List<>)))
                {
                    continue;
                }
                subClass.TrimStringProperties();
            }
        }

        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            while (oType != null)
            {
                if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                {
                    return true;
                }
                oType = oType.BaseType;
            }

            return false;
        }
    }
}
