using System;
using System.Collections.Generic;

namespace AoC
{
    public class AoCDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public TValue def = default;
        private bool _storeOnMissingLookup;
        private bool _hasDefaultConstructor;

        public AoCDictionary() : base()
        { 
            _hasDefaultConstructor = typeof(TValue).GetConstructor(Type.EmptyTypes) != null;
        }

        public AoCDictionary(bool storeOnMissingLookup) : this()
        {
            _storeOnMissingLookup = storeOnMissingLookup;
        }

        public AoCDictionary(TValue def, bool storeOnMissingLookup = false) : base()
        {
            this.def = def;
            _storeOnMissingLookup = storeOnMissingLookup;
        }
        public AoCDictionary(Dictionary<TKey, TValue> dict, bool storeOnMissingLookup = false) : base(dict)
        {
            _storeOnMissingLookup = storeOnMissingLookup;
            _hasDefaultConstructor = typeof(TValue).GetConstructor(Type.EmptyTypes) != null;
        }

        private TValue GetDefault() => _hasDefaultConstructor ? (TValue) Activator.CreateInstance(typeof(TValue))  : def; 

        new public TValue this[TKey key]
        { 
            get
            {
                if (!ContainsKey(key))
                {
                    var res = GetDefault();
                    if (!_storeOnMissingLookup)
                    {
                        return res;
                    }
                    Add(key, res);
                }
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
