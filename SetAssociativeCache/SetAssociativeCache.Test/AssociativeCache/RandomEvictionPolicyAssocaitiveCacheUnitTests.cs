using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test
{
    public class RandomEvictionPolicyAssocaitiveCacheUnitTests : AssociativeCacheUnitTests
    {
        public RandomEvictionPolicyAssocaitiveCacheUnitTests()
        {
            m_cache = new CustomEvictionAssociativeCache<string>(m_cacheSize, new RandomEvictionPolicy<string>());
        }
    }
}
