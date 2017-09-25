using System;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface ISetAssociativeCache<TKey, TValue>
    {
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Add(TKey key, TValue value);


        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        TValue Get(TKey key);

        int Size { get; }
    }
}
