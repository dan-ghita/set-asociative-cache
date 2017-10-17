using SetAssociativeCache.EvictionPolicy;

namespace SetAssociativeCache.Test
{
    public class LRUEvictionPolicyAssocaitiveCacheUnitTests : LRUAssocaitiveCacheUnitTests
    {
        public LRUEvictionPolicyAssocaitiveCacheUnitTests()
        {
            m_cache = new CustomEvictionAssociativeCache<string>(m_cacheSize, new LRUEvictionPolicy<string>());
        }
    }
}
