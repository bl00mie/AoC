using System.Collections.Generic;

namespace AoC
{
    public class AoCDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue this[TKey key]
        { 
            get
            {
                if (!ContainsKey(key)) Add(key, default);
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
