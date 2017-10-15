using System;
using System.Collections.Generic;

namespace SetAssociativeCache.Test.Shared
{
    public class FIFOEvictionPolicy<TValue> : IEvictionPolicy<TValue>
    {
        public void Evict(IList<CacheEntry<TValue>> container) => container.RemoveAt(0);
    }
}
