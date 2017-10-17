using SetAssociativeCache.EvictionPolicy;
using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class LRUEvictionPolicySetAssociativeCacheUnitTests : SetAssociativeLRUCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                () => new LRUEvictionPolicy<string>());
        }
    }
}