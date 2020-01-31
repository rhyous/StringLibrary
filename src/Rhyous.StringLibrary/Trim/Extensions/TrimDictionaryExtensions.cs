using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// Extensions for trimming strings in dictionary values.
    /// Todo: Trim keys as well as values.
    /// </summary>
    public static class TrimDictionaryExtensions
    {
        /// <summary>
        /// An extension method that trims string dictionary values or object dictionary values containing string.
        /// </summary>
        /// <param name="dictionary">The dictionary of strings or objects containing strings to trim.</param>
        /// <param name="objectsBeingTrimmed">A list of objects already being trimmed.</param>
        public static void TrimDictionary(this IDictionary dictionary, IList<object> objectsBeingTrimmed = null)
        {
            if (dictionary == null)
                return;
            objectsBeingTrimmed = objectsBeingTrimmed ?? new List<object>();
            objectsBeingTrimmed.Add(dictionary);
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
                    if (type.IsTrimmable() && type.IsSupportedCollection())
                        dictionary[key].TrimStringProperties(objectsBeingTrimmed);
                }
            }
        }
    }
}