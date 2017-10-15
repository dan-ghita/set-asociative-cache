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
        /// Evicts element from container.
        /// </summary>
        /// <param name="container">The container.</param>
        void Evict(IList<CacheEntry<TValue>> container);
    }
}
