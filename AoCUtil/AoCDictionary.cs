using System.Collections.Generic;

namespace AoC
{
    public class AoCDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue def = default;

        public AoCDictionary() : base() { }
        public AoCDictionary(TValue def) : base()
        {
            this.def = def;
        }

        public TValue this[TKey key]
        { 
            get
            {
                if (!ContainsKey(key)) Add(key, def);
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
