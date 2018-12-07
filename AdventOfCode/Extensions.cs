using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Extensions
    {
        public static HashSet<T> GetValueAndAddKeyIfNotExist<T>(this Dictionary<string, HashSet<T>> dictionary, string key)
        {
            HashSet<T> list;
            if (!dictionary.TryGetValue(key, out list))
            {
                list = new HashSet<T>();
                dictionary.Add(key, list);
            }
            return list ?? new HashSet<T>();
        }
    }
}
