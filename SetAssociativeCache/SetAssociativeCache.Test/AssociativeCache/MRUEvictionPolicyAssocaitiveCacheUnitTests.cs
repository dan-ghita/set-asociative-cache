using SetAssociativeCache.EvictionPolicy;

namespace SetAssociativeCache.Test
{
    public class MRUEvictionPolicyAssocaitiveCacheUnitTests : MRUAssocaitiveCacheUnitTests
    {
        public MRUEvictionPolicyAssocaitiveCacheUnitTests()
        {
            m_cache = new CustomEvictionAssociativeCache<string>(m_cacheSize, new MRUEvictionPolicy<string>());
        }
    }
}
