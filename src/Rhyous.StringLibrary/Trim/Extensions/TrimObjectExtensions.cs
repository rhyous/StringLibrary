using System;
using System.Collections;
using System.Collections.Generic;
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
        /// <param name="obj">The object to trim.</param>
        /// <param name="objectsBeingTrimmed">A list of objects already being trimmed.</param>
        public static void TrimStringProperties(this object obj, IList<object> objectsBeingTrimmed = null)
        {
            if (obj == null || obj.IsAlreadyBeingTrimmed(objectsBeingTrimmed))
                return;
            var type = obj.GetType();
            if (!type.IsTrimmable() || type == typeof(string)) // We can't trim a string if it is the root object as a string is immutable
                return;
            objectsBeingTrimmed = objectsBeingTrimmed ?? new List<object>();
            objectsBeingTrimmed.Add(obj);

            if (obj.IsSupportedCollection())
                obj.TrimCollection(objectsBeingTrimmed);

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
                if (prop.PropertyType.IsSupportedCollection())
                {
                    try
                    {
                        var collection = prop.GetValue(obj, null);
                        collection.TrimCollection(objectsBeingTrimmed);
                        continue;
                    }
                    catch (Exception) { /* Ok. Fine. We won't trim. This exception is intentionally swallowed. */ }
                }
                try
                {
                    var value = prop.GetValue(obj, null);
                    if (value != null)
                    {
                        value.TrimStringProperties(objectsBeingTrimmed);
                    }
                }
                catch (Exception) { /* Ok. Fine. We won't trim. This exception is intentionally swallowed. */ }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objectsBeingTrimmed"></param>
        public static void TrimCollection(this object obj, IList<object> objectsBeingTrimmed = null)
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

        /// <summary>
        /// A method that trims an object.
        /// </summary>
        /// <param name="obj">The object to trim.</param>
        /// <param name="prop">The PropertyInfo of that object.</param>
        /// <param name="objectsBeingTrimmed">A list of objects already being trimed.</param>
        public static void TrimObject(this object obj, PropertyInfo prop, IList<object> objectsBeingTrimmed = null)
        {
            try
            {
                var value = prop.GetValue(obj, null);
                if (value.GetType() == typeof(string))
                    obj.TrimString(prop);
                else
                    value.TrimStringProperties(objectsBeingTrimmed);
            }
            catch (Exception) { /* Ok. Fine. We won't trim. This exception is intentionally swallowed. */ }
        }

        /// <summary>
        /// A method to trim an property value that is a string.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="stringProperty">The string property of an object.</param>
        public static void TrimString(this object obj, PropertyInfo stringProperty)
        {
            string currentValue;
            try
            {
                if (stringProperty.GetSetMethod() == null || (currentValue = (string)stringProperty.GetValue(obj, null)) == null)
                    return;
                stringProperty.SetValue(obj, currentValue.TrimAll(), null);
            }
            catch (Exception) { /* Ok. Fine. We won't trim. This exception is intentionally swallowed. */ }
        }

        internal static bool IsSupportedCollection(this object o)
        {
            return o.GetType().IsSupportedCollection();
        }

        internal static bool IsAlreadyBeingTrimmed(this object obj, IList<object> objectsBeingTrimmed)
        {
            return (objectsBeingTrimmed != null && objectsBeingTrimmed.Count > 0 && objectsBeingTrimmed.Contains(obj));
        }

    }
}
