using System;
using System.Collections;
using Moq;
using SetAssociativeCache.Shared;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeLRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public SetAssociativeLRUCacheUnitTests()
        {
            m_cache = new SetAssociativeCache<string, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new LRUAssociativeCache<string>(size), m_bitConverter);
        }

        public override void RegisterCacheWithMock(Mock<IBitConverter> bitConverterMock)
        {
            // 1 * 1024 / 64 = 16 -> first 4 bit for set index
            ISetAssociativeCache<string, string> m_cache = new SetAssociativeCache<string, string>(1, 1,
                (size) => new LRUAssociativeCache<string>(size), bitConverterMock.Object);
        }
    }
}