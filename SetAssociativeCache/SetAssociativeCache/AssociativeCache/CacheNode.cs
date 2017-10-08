namespace SetAssociativeCache
{
    /// <summary>
    /// Cache node for LRU and MRU implementations
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class CacheNode<TValue>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        public CacheEntry<TValue> Value { get; private set; }


        /// <summary>
        /// Gets or sets the previous node.
        /// </summary>        
        /// <returns>Previous node.</returns>
        public CacheNode<TValue> Previous { get; set; }


        /// <summary>
        /// Gets or sets the next node.
        /// </summary>        
        /// <returns>Next node.</returns>
        public CacheNode<TValue> Next { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheNode{TValue}"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public CacheNode(int tag, TValue value)
        {
            Value = new CacheEntry<TValue>(tag, value);
            Previous = null;
            Next = null;
        }
    }
}
