using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class DictionaryHelper
    {
        public static string DictionaryToString<K, V>(this IDictionary<K, V> dict)
        {
            var items = dict.Select(kvp => kvp.ToString());
            return string.Join(", ", items);
        }

        public static IList<K> GetKeys<K, V>(this IDictionary<K, V> dict)
        {
            return dict.Select(s => s.Key).ToList();
        }
    }
}
