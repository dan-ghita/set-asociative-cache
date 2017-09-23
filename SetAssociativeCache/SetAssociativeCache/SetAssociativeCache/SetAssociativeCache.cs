
using BinaryFormatter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Marshal = System.Runtime.InteropServices.Marshal;

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
        private IList<IAssociativeCache<TValue>> associativeCacheSet;


        /// <summary>
        /// Initializes a new instance of the <see cref="SetAssociativeCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="cacheSizeInKb">The cache size in kb.</param>
        /// <param name="numberOfWays">The number of ways.</param>
        /// <param name="associativeCacheFactory">The associative cache factory.</param>
        public SetAssociativeCache(int cacheSizeInKb, int numberOfWays, Func<int, IAssociativeCache<TValue>> associativeCacheFactory)
        {
            int sizeOfBlock = Marshal.SizeOf(default(TValue));
            int setCount = cacheSizeInKb / numberOfWays / sizeOfBlock;

            associativeCacheSet = Enumerable.Range(0, setCount).Select(i => associativeCacheFactory(numberOfWays)).ToList();    
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
            => associativeCacheSet.ElementAt(GetSetIndex(key)).Add(GetTag(key), value);


        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue Get(TKey key)
            => associativeCacheSet.ElementAt(GetSetIndex(key)).Get(GetTag(key));


        private int GetSetIndex(TKey key)
        {
            return 0;
        }


        private int GetTag(TKey key)
        {
            return 0;
        }
    }
}
