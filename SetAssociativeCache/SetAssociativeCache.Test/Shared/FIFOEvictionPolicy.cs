using System;
using System.Collections.Generic;

namespace SetAssociativeCache.Test.Shared
{
    public class FIFOEvictionPolicy<TValue> : IEvictionPolicy<TValue>
    {
        private int index = -1;

        public int GetIndexToEvict(IList<CacheEntry<TValue>> container)
            => index = ++index % container.Count;
    }
}
