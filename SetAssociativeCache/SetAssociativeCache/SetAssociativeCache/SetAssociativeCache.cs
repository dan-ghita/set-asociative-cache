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
        /// Initializes a new instance of the <see cref="SetAssociativeCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="cacheSizeInKb">The cache size in kb.</param>
        /// <param name="numberOfWays">The number of ways.</param>
        /// <param name="associativeCacheFactory">The associative cache factory.</param>
        public SetAssociativeCache(int numberOfSets, int numberOfWays, Func<int, IAssociativeCache<TValue>> associativeCacheFactory)
        {
            associativeCacheSet = new Lazy<IList<IAssociativeCache<TValue>>>(
                () => Enumerable.Range(0, numberOfSets).Select(i => associativeCacheFactory(numberOfWays)).ToList());
        }


        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
            => associativeCacheSet.Value.ElementAt(GetSetIndex(key)).Add(GetTag(key), value);


        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue Get(TKey key)
            => associativeCacheSet.Value.ElementAt(GetSetIndex(key)).Get(GetTag(key));

        
        /// <summary>
        /// Gets the count of elements in the cache
        /// </summary>
        public int Size
            => associativeCacheSet.Value.Sum(set => set.Size);


        private int GetSetIndex(TKey key)
            => Math.Abs(key.GetHashCode() % associativeCacheSet.Value.Count);


        private int GetTag(TKey key)
            => key.GetHashCode() - GetSetIndex(key);
    }
}
