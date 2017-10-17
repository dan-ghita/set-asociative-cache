﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SetAssociativeCache
{
    /// <summary>
    /// Custom eviction associative cache implementation
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="SetAssociativeCache.IAssociativeCache{TValue}" />
    public class CustomEvictionAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEvictionAssociativeCache{TValue}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="evictionPolicy">The eviction policy.</param>
        public CustomEvictionAssociativeCache(int size, IEvictionPolicy<TValue> evictionPolicy)
        {
            m_cacheSize = size;
            m_evictionPolicy = evictionPolicy;
            m_container = new List<CacheEntry<TValue>>();
        }

        /// <summary>
        /// Add element to cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public void Add(int tag, TValue value)
        {
            if (m_container.Where(element => element.Tag.Equals(tag)).Count() == 0)
                Insert(tag, value);
        }


        /// <summary>
        /// Get element from cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public TValue Get(int tag) => m_container.Any(element => element.Tag.Equals(tag))
            ? m_container.Where(element => element.Tag.Equals(tag)).First().Data
            : default(TValue);


        /// <summary>
        /// Counts the elements in cache.
        /// </summary>
        public int Count => m_container.Count();


        /// <summary>
        /// Flushes the cache.
        /// </summary>
        public void Clear() => m_container.Clear();


        private void Insert(int tag, TValue value)
        {
            CacheEntry<TValue> newEntry = new CacheEntry<TValue>(tag, value);

            if (m_container.Count() == m_cacheSize)
                m_container[m_evictionPolicy.GetIndexToEvict(m_container)] = newEntry;
            else
                m_container.Add(newEntry);
        }


        private int m_cacheSize;


        private IEvictionPolicy<TValue> m_evictionPolicy;


        private IList<CacheEntry<TValue>> m_container;
    }
}