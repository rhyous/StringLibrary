using System.Collections;
using System.Collections.Generic;

namespace Rhyous.StringLibrary
{
    public static class ListExtensions
    {
        public static void TrimList(this IList list, IList<object> objectsBeingTrimmed)
        {
            objectsBeingTrimmed = objectsBeingTrimmed ?? new List<object>();
            objectsBeingTrimmed.Add(list);
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item != null && item.GetType() == typeof(string))
                    list[i] = item.ToString().TrimAll();
                else
                    item.TrimStringProperties(objectsBeingTrimmed);
            }
        }
    }
}
