using System;
using System.Collections;
using System.Collections.Generic;
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
            //int sizeOfBlockInBytes = Marshal.SizeOf(typeof(TValue));
            int sizeOfBlockInBytes = 64;
            int setCount = cacheSizeInKb * 1024 / sizeOfBlockInBytes / numberOfWays;

            associativeCacheSet = Enumerable.Range(0, setCount).Select(i => associativeCacheFactory(numberOfWays)).ToList();
            numberOfSetBits = new Lazy<int>(() => (int)Math.Log(associativeCacheSet.Count(), 2));
        }


        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            BitArray bits = BitConverter.ObjectToBits(key);

            associativeCacheSet.ElementAt(GetSetIndex(bits)).Add(GetTag(bits), value);
        }


        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue Get(TKey key)
        {
            BitArray bits = BitConverter.ObjectToBits(key);

            return associativeCacheSet.ElementAt(GetSetIndex(bits)).Get(GetTag(bits));
        }


        private int GetSetIndex(BitArray bits)
        {
            BitArray setBits = new BitArray(numberOfSetBits.Value);

            for (int i = 0; i < numberOfSetBits.Value; ++i)
                setBits.Set(i, bits.Get(i));

            return BitConverter.ConvertToInt(setBits);
        }


        private BitArray GetTag(BitArray bits)
        {
            BitArray tagBits = new BitArray(bits.Length - numberOfSetBits.Value);

            for (int i = numberOfSetBits.Value; i < bits.Length; ++i)
                tagBits.Set(i - numberOfSetBits.Value, bits.Get(i));

            return tagBits;
        }


        private Lazy<int> numberOfSetBits;
    }
}
