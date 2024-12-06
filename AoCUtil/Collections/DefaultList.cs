using System;
using System.Collections.Generic;

namespace AoCUtil.Collections
{
    public class DefaultList<T> : List<T>
    {
        private readonly T _default = default;
        private readonly bool _storeOnLookup;
        private readonly bool _hasDefaultConstructor;

        public DefaultList() : base()
        {
            _storeOnLookup = false;
            _hasDefaultConstructor = typeof(T).GetConstructor(Type.EmptyTypes) != null;
        }

        public DefaultList(bool storeOnLookup)
        {
            _storeOnLookup= storeOnLookup;
        }

        public DefaultList(T @default, bool storeOnLookup = false) : this()
        {
            _default = @default;
            _storeOnLookup = storeOnLookup;
        }

        public DefaultList(IEnumerable<T> toCopy, T @default = default, bool storeOnLookup = false) :base(toCopy)
        {
            _default = @default;
            _storeOnLookup = storeOnLookup;
            _hasDefaultConstructor = typeof(T).GetConstructor(Type.EmptyTypes) != null;
        }

        private T GetDefault() => _hasDefaultConstructor ? (T)Activator.CreateInstance(typeof(T)) : _default;

        new public T this[int index]
        {
            get
            {
                if (index > Count)
                    if (_storeOnLookup)
                        this[index] = GetDefault();
                    else 
                        return GetDefault();
                return base[index];
            }
            set
            {
                if (index > Count)
                {
                    for (int i = Count; i < index; i++)
                        Add(GetDefault());
                    Add(value);
                }
                else
                    base[index] = value;
            }
        }
    }
}
