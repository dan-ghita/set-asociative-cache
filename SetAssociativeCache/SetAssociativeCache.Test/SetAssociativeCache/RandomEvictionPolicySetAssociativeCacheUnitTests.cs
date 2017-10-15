using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class RandomEvictionPolicySetAssociativeCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                new RandomEvictionPolicy<string>());
        }
    }
}