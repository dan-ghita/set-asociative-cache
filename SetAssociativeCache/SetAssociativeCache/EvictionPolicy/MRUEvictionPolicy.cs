using System;
using System.Collections.Generic;
using System.Linq;

namespace SetAssociativeCache.EvictionPolicy
{
    /// <summary>
    /// MRU eviction policy
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="SetAssociativeCache.IEvictionPolicy{TValue}" />
    public class MRUEvictionPolicy<TValue> : IEvictionPolicy<TValue>
    {
        /// <summary>
        /// Returns index which should be evicted.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>Index to evict</returns>
        public int GetIndexToEvict(List<CacheEntry<TValue>> container)
        {
            long maxAccessTime = container.Max(x => x.AccessTime);

            return container.FindIndex(element => element.AccessTime == maxAccessTime);
        }
    }
}
