using System;
using System.Collections;
using System.Reflection;

namespace Rhyous.StringLibrary
{ 
    /// <summary>
     /// Extensions for trimming string properties of any object. No matter how big the object,
     /// how many string properties, or how many sub objects with string roperties, you can trim
     /// and entire object with one line of code:
     ///     obj.TrimStringProperties();
     /// </summary>
    public static class TrimObjectExtensions
    {
        /// <summary>
        /// Finds any property of type string and trims it, unless that property has the
        /// IgnoreTrimAttribute applied.
        /// </summary>
        /// <param name="obj"></param>
        public static void TrimStringProperties(this object obj)
        {
            var type = obj.GetType();
            if (!type.IsTrimmable() || type == typeof(string)) // We can't trim a string if it is the root object as a string is immutable
                return;

            if (type.IsArray)
                obj.TrimArray();

            if (obj.IsSupportedGeneric())
                obj.TrimGeneric();

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            foreach (var prop in properties)
            {
                if (!prop.IsTrimmable())
                {
                    continue;
                }
                if (prop.PropertyType == typeof(object))
                {
                    obj.TrimObject(prop);
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
                    generic.TrimGeneric();
                    continue;
                }

                prop.GetValue(obj, null).TrimStringProperties();
            }
        }

        public static void TrimArray(this object obj)
        {
            var array = obj as Array;
            if (array != null & array.Length == 0)
                return;
            array.TrimList();
        }

        public static void TrimGeneric(this object obj)
        {
            var dictionary = obj as IDictionary;
            if (dictionary != null)
            {
                dictionary.TrimDictionary();
                return;
            }
            var list = obj as IList;
            if (list != null)
            {
                list.TrimList();
                return;
            }
        }        

        public static void TrimObject(this object obj, PropertyInfo prop)
        {
            var value = prop.GetValue(obj, null);
            if (value.GetType() == typeof(string))
                obj.TrimString(prop);
            else
                value.TrimStringProperties();
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

    }
}
