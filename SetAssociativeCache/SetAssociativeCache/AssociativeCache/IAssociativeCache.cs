namespace SetAssociativeCache
{
    /// <summary>
    /// Associative cache interface
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IAssociativeCache<TValue>
    {
        /// <summary>
        /// Add element to cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        void Add(int tag, TValue value);


        /// <summary>
        /// Get element from cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>Value if element is found, null otherwise</returns>
        TValue Get(int tag);


        /// <summary>
        /// Counts the elements in cache.
        /// </summary>
        /// <returns>The size.</returns>
        int Count { get; }
    }
}
