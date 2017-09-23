using System.Collections;

namespace SetAssociativeCache
{
    public class CacheEntry<TValue>
    {
        public CacheEntry(BitArray tag, TValue value)
        {
            Tag = tag;
            Data = value;
        }
        
        public BitArray Tag { get; private set; }

        public TValue Data { get; private set; }
    }
}
