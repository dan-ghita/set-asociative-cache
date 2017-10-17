using System;

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

            UpdateAccessTime();
        }

        /// <summary>
        /// Update access time
        /// </summary>
        public void UpdateAccessTime() => AccessTime = DateTime.Now; 

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <return>The tag.</return>
        public int Tag { get; private set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <return>The Data.</return>
        public TValue Data { get; set; }

        /// <summary>
        /// Last access time
        /// </summary>
        /// <return>The access time.</return>
        public DateTime AccessTime;
    }
}
