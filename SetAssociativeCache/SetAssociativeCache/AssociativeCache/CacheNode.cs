using System.Collections;

namespace SetAssociativeCache
{
    public class CacheNode<TValue>
    {
        public CacheEntry<TValue> Value { get; private set; }
        public CacheNode<TValue> Previous { get; set; }
        public CacheNode<TValue> Next { get; set; }

        public CacheNode(int tag, TValue value)
        {
            Value = new CacheEntry<TValue>(tag, value);
            Previous = null;
            Next = null;
        }
    }
}
