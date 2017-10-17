using System.Collections.Generic;

namespace SetAssociativeCache
{
    /// <summary>
    /// Eviction policy implementation
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IEvictionPolicy<TValue>
    {
        /// <summary>
        /// Returns index which should be evicted.
        /// </summary>
        /// <param name="container">The container.</param>
        int GetIndexToEvict(IList<CacheEntry<TValue>> container);
    }
}
