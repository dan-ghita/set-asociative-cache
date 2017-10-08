using System;
using System.Collections.Generic;
using System.Linq;

namespace SetAssociativeCache.Test.Shared
{
    class NoEvictionAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        private List<CacheEntry<TValue>> m_container = new List<CacheEntry<TValue>>();

        private int m_size;

        public NoEvictionAssociativeCache(int size) => m_size = size;

        public int Count => m_container.Count;

        public void Add(int tag, TValue value)
        {
            if(m_container.Count < m_size)
                m_container.Add(new CacheEntry<TValue>(tag, value));
        }

        public TValue Get(int tag) => m_container.Any(element => element.Tag.Equals(tag))
            ? m_container.Find(element => element.Tag.Equals(tag)).Data
            : default(TValue);

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
