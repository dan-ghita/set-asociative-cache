using SetAssociativeCache.EvictionPolicy;
using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class MRUEvictionPolicySetAssociativeCacheUnitTests : SetAssociativeMRUCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                () => new MRUEvictionPolicy<string>());
        }
    }
}