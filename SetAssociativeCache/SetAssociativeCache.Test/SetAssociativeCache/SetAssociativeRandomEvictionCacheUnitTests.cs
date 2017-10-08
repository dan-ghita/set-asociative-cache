using SetAssociativeCache.Test.Shared;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeRandomEvictionCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new RandomEvictionAssociativeCache<string>(size));
        }
    }
}