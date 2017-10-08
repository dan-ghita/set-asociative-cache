using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeLRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                (size) => new LRUAssociativeCache<string>(size));
        }
    }
}