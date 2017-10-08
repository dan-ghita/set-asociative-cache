namespace SetAssociativeCache
{
    /// <summary>
    /// Cache entry
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class CacheEntry<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheEntry{TValue}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public CacheEntry(int tag, TValue value)
        {
            Tag = tag;
            Data = value;
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <return>The tag.</return>
        public int Tag { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <return>The Data.</return>
        public TValue Data { get; private set; }
    }
}
