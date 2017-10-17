using System;
using System.Collections.Generic;

namespace SetAssociativeCache.Test.Shared
{
    public class RandomEvictionPolicy<TValue> : IEvictionPolicy<TValue>
    {
        public int GetIndexToEvict(IList<CacheEntry<TValue>> container)
            => new Random().Next() % container.Count;
    }
}
