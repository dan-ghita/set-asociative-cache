using System;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeRandomEvictionCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public SetAssociativeRandomEvictionCacheUnitTests()
        {
            m_cache = new SetAssociativeCache<string, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new RandomEvictionAssociativeCache<string>(size));
        }
    }
}