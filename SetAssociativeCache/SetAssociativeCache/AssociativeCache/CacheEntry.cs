using System.Collections;

namespace SetAssociativeCache
{
    public class CacheEntry<TValue>
    {
        public CacheEntry(int tag, TValue value)
        {
            Tag = tag;
            Data = value;
        }
        
        public int Tag { get; private set; }

        public TValue Data { get; private set; }
    }
}
