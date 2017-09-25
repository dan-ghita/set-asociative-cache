using SetAssociativeCache.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetAssociativeCache
{
    /// <summary>
    /// LRU associative cache implementation
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="SetAssociativeCache.IAssociativeCache{TValue}" />
    public class RandomEvictionAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LRUAssociativeCache{Tkey, TValue}" /> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public RandomEvictionAssociativeCache(int size)
        {
            m_cacheSize = size;
            m_container = new List<CacheEntry<TValue>>();
        }

        /// <summary>
        /// Add element to cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public void Add(BitArray tag, TValue value)
        {
            if(m_container.Where(element => element.Tag.Equals(tag)).Count() == 0)
            {
                Insert(tag, value);
            }
        }


        /// <summary>
        /// Get element from cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public TValue Get(BitArray tag) => m_container.Any(element => element.Tag.IsEqual(tag))
            ? m_container.Where(element => element.Tag.IsEqual(tag)).First().Data
            : default(TValue);


        /// <summary>
        /// Sizes this instance.
        /// </summary>
        /// <returns></returns>
        public int Size => m_container.Count();


        private void Insert(BitArray tag, TValue value)
        {
            if(m_container.Count() == m_cacheSize)
            {
                Random random = new Random();
                m_container.RemoveAt(random.Next(m_cacheSize));
            }

            m_container.Add(new CacheEntry<TValue>(tag, value));
        }

        
        private int m_cacheSize;
        

        private IList<CacheEntry<TValue>> m_container;
    }
}
