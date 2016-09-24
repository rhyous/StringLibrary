using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary
{
    public static class DictionaryExtensions
    {
        public static void TrimDictionary(this IDictionary dictionary, IList<object> objectsBeingTrimmed)
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
                    if (type.IsTrimmable() && type.IsSupportedGeneric())
                        dictionary[key].TrimStringProperties(objectsBeingTrimmed);
                }
            }
        }
    }
}
