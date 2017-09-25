using System;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeMRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public SetAssociativeMRUCacheUnitTests()
        {
            m_cache = new SetAssociativeCache<string, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new MRUAssociativeCache<string>(size), m_bitConverter);
        }
    }
}