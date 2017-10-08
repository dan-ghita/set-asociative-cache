namespace SetAssociativeCache
{
    /// <summary>
    /// Set associative cache interface
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface ISetAssociativeCache<TKey, TValue>
    {
        /// <summary>
        /// Adds element to cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Add(TKey key, TValue value);


        /// <summary>
        /// Gets element from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Value of the element if found, null otherwise.</returns>
        TValue Get(TKey key);


        /// <summary>
        /// Counts the elements in the cache.
        /// </summary>
        /// <returns>The count.</returns>
        int Count { get; }
    }
}
