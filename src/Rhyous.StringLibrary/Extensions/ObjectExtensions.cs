using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Finds any property of type string and trims it, unless that property has the
        /// IgnoreTrimAttribute applied.
        /// </summary>
        /// <param name="obj"></param>
        public static void TrimStringProperties(this object obj, IList<object> objectsBeingTrimmed = null)
        {
            if (obj == null || obj.IsAlreadyBeingTrimmed(objectsBeingTrimmed))
                return;
            var type = obj.GetType();
            if (!type.IsTrimmable() || type == typeof(string)) // We can't trim a string if it is the root object as a string is immutable
                return;
            objectsBeingTrimmed = objectsBeingTrimmed ?? new List<object>();
            objectsBeingTrimmed.Add(obj);

            if (obj.IsSupportedGeneric())
                obj.TrimGeneric(objectsBeingTrimmed);

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            foreach (var prop in properties)
            {
                if (!prop.IsTrimmable())
                {
                    continue;
                }
                if (prop.PropertyType == typeof(object))
                {
                    obj.TrimObject(prop, objectsBeingTrimmed);
                    continue;
                }
                if (prop.PropertyType == typeof(string))
                {
                    obj.TrimString(prop);
                    continue;
                }
                if (prop.PropertyType.IsSupportedGeneric())
                {
                    var generic = prop.GetValue(obj, null);
                    generic.TrimGeneric(objectsBeingTrimmed);
                    continue;
                }
                var value = prop.GetValue(obj, null);
                if (value != null)
                {
                    value.TrimStringProperties(objectsBeingTrimmed);
                }
            }
        }

        public static void TrimGeneric(this object obj, IList<object> objectsBeingTrimmed = null)
        {
            var dictionary = obj as IDictionary;
            if (dictionary != null)
            {
                dictionary.TrimDictionary(objectsBeingTrimmed);
                return;
            }
            var list = obj as IList;
            if (list != null)
            {
                list.TrimList(objectsBeingTrimmed);
                return;
            }
        }

        public static void TrimObject(this object obj, PropertyInfo prop, IList<object> objectsBeingTrimmed = null)
        {
            var value = prop.GetValue(obj, null);
            if (value.GetType() == typeof(string))
                obj.TrimString(prop);
            else
            {
                value.TrimStringProperties(objectsBeingTrimmed);
            }
        }

        public static void TrimString(this object obj, PropertyInfo stringProperty)
        {
            string currentValue;
            if (stringProperty.GetSetMethod() == null || (currentValue = (string)stringProperty.GetValue(obj, null)) == null)
                return;
            stringProperty.SetValue(obj, currentValue.TrimAll(), null);
        }

        internal static bool IsSupportedGeneric(this object o)
        {
            return o.GetType().IsSupportedGeneric();
        }

        internal static bool IsAlreadyBeingTrimmed(this object obj, IList<object> objectsBeingTrimmed)
        {
            return (objectsBeingTrimmed != null && objectsBeingTrimmed.Count > 0 && objectsBeingTrimmed.Contains(obj));
        }

    }
}
