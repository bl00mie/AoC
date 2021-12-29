using System.Collections.Generic;

namespace AoC
{
    public class AoCDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue def = default;
        private bool _storeOnMissingLookup;

        public AoCDictionary() : base() { }
        public AoCDictionary(TValue def, bool storeOnMissingLookup = false) : base()
        {
            this.def = def;
            _storeOnMissingLookup = storeOnMissingLookup;
        }

        new public TValue this[TKey key]
        { 
            get
            {
                if (!ContainsKey(key))
                {
                    if (!_storeOnMissingLookup)
                    {
                        return def;
                    }
                    Add(key, def);
                }
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
