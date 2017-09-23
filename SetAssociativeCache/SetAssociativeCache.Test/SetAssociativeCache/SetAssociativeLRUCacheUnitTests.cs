using System;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeLRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public SetAssociativeLRUCacheUnitTests()
        {
            m_cache = new SetAssociativeCache<string, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new LRUAssociativeCache<string>(size));
        }
    }
}