using System.Collections;

namespace Rhyous.StringLibrary
{    /// <summary>
     /// Extensions for trimming strings in ILists.
     /// </summary>
    public static class TrimListExtensions
    {
        public static void TrimList(this IList array)
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
    }
}
