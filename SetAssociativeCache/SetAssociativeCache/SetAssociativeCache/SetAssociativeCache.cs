using System;
using System.Collections.Generic;
using System.Linq;

namespace SetAssociativeCache
{
    /// <summary>
    /// Set associative cache
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="SetAssociativeCache.ISetAssociativeCache{TKey, TValue}" />
    public class SetAssociativeCache<TKey, TValue> : ISetAssociativeCache<TKey, TValue>
    {
        /// <summary>
        /// The associative cache set
        /// </summary>
        private Lazy<IList<IAssociativeCache<TValue>>> associativeCacheSet;


        /// <summary>
        /// Initializes a new instance of the <see cref="SetAssociativeCache{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="numberOfSets">The number of sets.</param>
        /// <param name="numberOfWays">The number of ways.</param>
        /// <remarks>Default replacement strategy is LRU</remarks>
        public SetAssociativeCache(int numberOfSets, int numberOfWays) : this(numberOfSets, numberOfWays, (size) => new LRUAssociativeCache<TValue>(size)) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="SetAssociativeCache{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="numberOfSets">The number of sets.</param>
        /// <param name="numberOfWays">The number of ways.</param>
        /// <param name="associativeCacheFactory">The associative cache factory.</param>
        public SetAssociativeCache(int numberOfSets, int numberOfWays, Func<int, IAssociativeCache<TValue>> associativeCacheFactory)
        {
            m_numberOfSetBits = (int)Math.Log(numberOfSets, 2) + numberOfSets & 1;

            associativeCacheSet = new Lazy<IList<IAssociativeCache<TValue>>>(
                () => Enumerable.Range(0, numberOfSets).Select(i => associativeCacheFactory(numberOfWays)).ToList());
        }


        /// <summary>
        /// Adds element to cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value) => associativeCacheSet.Value.ElementAt(GetSetIndex(key)).Add(GetTag(key), value);


        /// <summary>
        /// Gets element from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Value of the element if found, null otherwise.</returns>
        public TValue Get(TKey key) => associativeCacheSet.Value.ElementAt(GetSetIndex(key)).Get(GetTag(key));


        /// <summary>
        /// Counts the elements in the cache.
        /// </summary>
        /// <returns>The count.</returns>
        public int Count => associativeCacheSet.Value.Sum(set => set.Count);


        /// <summary>
        /// Flushes the cache.
        /// </summary>
        public void Clear() => associativeCacheSet.Value.ToList().ForEach(set => set.Clear());


        private int GetSetIndex(TKey key) => Math.Abs(key.GetHashCode() % associativeCacheSet.Value.Count());


        private int GetTag(TKey key) => key.GetHashCode() >> m_numberOfSetBits;


        private int m_numberOfSetBits;
    }
}
