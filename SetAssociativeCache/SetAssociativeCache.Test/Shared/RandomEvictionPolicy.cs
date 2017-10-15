using System;
using System.Collections.Generic;

namespace SetAssociativeCache.Test.Shared
{
    public class RandomEvictionPolicy<TValue> : IEvictionPolicy<TValue>
    {
        public void Evict(IList<CacheEntry<TValue>> container)
            => container.RemoveAt(new Random().Next() % container.Count);
    }
}
