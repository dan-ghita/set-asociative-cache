using System.Collections.Generic;

namespace SetAssociativeCache
{
    public abstract class RUAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        protected IDictionary<int, CacheNode<TValue>> nodePointer;
        protected CacheNode<TValue> root, tail;


        /// <summary>
        /// Initializes a new instance of the <see cref="RUAssociativeCache{TValue}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public RUAssociativeCache(int size)
        {
            m_cacheSize = size;
            nodePointer = new Dictionary<int, CacheNode<TValue>>();

            root = new CacheNode<TValue>(-1, default(TValue));
            tail = new CacheNode<TValue>(-1, default(TValue));

            root.Next = tail;
            tail.Previous = root;
        }


        /// <summary>
        /// Add element to cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="value">The value.</param>
        public void Add(int tag, TValue value)
        {
            if (nodePointer.ContainsKey(tag))
            {
                MoveToFront(nodePointer[tag]);
            }
            else
            {
                CacheNode<TValue> newNode = new CacheNode<TValue>(tag, value);

                if(Count == m_cacheSize)
                    ReplaceRecentlyUsed(newNode);
                else
                    AddToFront(newNode);

                nodePointer.Add(tag, newNode);
            }
        }


        /// <summary>
        /// Get element from cache
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>
        /// Value if element is found, null otherwise
        /// </returns>
        public TValue Get(int tag)
        {
            if (nodePointer.ContainsKey(tag))
            {
                MoveToFront(nodePointer[tag]);
                return nodePointer[tag].Value.Data;
            }
            else
            {
                return default(TValue);
            }
        }


        /// <summary>
        /// Counts the elements in cache.
        /// </summary>
        public int Count => nodePointer.Count;


        /// <summary>
        /// Flushes the cache.
        /// </summary>
        public void Clear()
        {
            nodePointer.Clear();
            root.Next = tail;
            tail.Previous = root;
        }


        /// <summary>
        /// Replaces the recently used.
        /// </summary>
        /// <param name="node">The node.</param>
        protected abstract void ReplaceRecentlyUsed(CacheNode<TValue> node);


        /// <summary>
        /// Adds to front.
        /// </summary>
        /// <param name="node">The node.</param>
        protected void AddToFront(CacheNode<TValue> node)
        {
            node.Next = root.Next;
            node.Previous = root;
            root.Next.Previous = node;
            root.Next = node;
        }


        private void MoveToFront(CacheNode<TValue> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            AddToFront(node);
        }


        private int m_cacheSize;
    }
}
