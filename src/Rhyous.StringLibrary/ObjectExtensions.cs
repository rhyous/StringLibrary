using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rhyous.StringLibrary
{
    public static class ObjectExtensions
    {
        public static List<Type> SupportedGenericTypes = new List<Type> {
            typeof(IList),
            typeof(IDictionary),
            typeof(IList<>),
            typeof(IDictionary<,>)
        };

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

        private static void TrimList(this IList array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                var item = array[i];
                if (item != null && item.GetType() == typeof(string))
                    array[i] = item.ToString().TrimAll();
                else
                    item.TrimStringProperties();
            }
        }

        public static void TrimGeneric(this object obj)
        {
            var dictionary = obj as IDictionary;
            if (dictionary != null)
            {
                TrimDictionary(dictionary);
                return;
            }
            var list = obj as IList;
            if (list != null)
            {
                list.TrimList();
                return;
            }
        }

        private static void TrimDictionary(IDictionary dictionary)
        {
            if (dictionary == null)
                return;
            var list = new List<object>();
            foreach (var item in dictionary.Keys)
                list.Add(item);
            foreach (var key in list)
            {
                var value = dictionary[key];
                var type = value.GetType();
                if (type == typeof(string))
                {
                    dictionary[key] = ((string)dictionary[key]).TrimAll();
                }
                else
                {
                    if (type.IsTrimmable() && type.IsSupportedGeneric())
                        dictionary[key].TrimStringProperties();
                }
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

        public static bool IsTrimmable(this PropertyInfo pi)
        {
            if (pi == null)
                return false;
            var indexParams = pi.GetIndexParameters();
            return (indexParams == null || indexParams.Length == 0 && pi.PropertyType.IsTrimmable());
        }

        internal static bool IsTrimmable(this Type type)
        {
            return !type.IsPrimitive && (!type.IsGenericType || type.IsSupportedGeneric());
        }

        internal static bool IsSupportedGeneric(this object o)
        {
            return o.GetType().IsSupportedGeneric();
        }

        internal static bool IsSupportedGeneric(this Type type)
        {
            while (type != null)
            {
                if (SupportedGenericTypes.Contains(type)
                    || (type.IsGenericType && (SupportedGenericTypes.Contains(type.GetGenericTypeDefinition())))
                    || type.GetInterfaces().Any(i => SupportedGenericTypes.Contains(i)))
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
