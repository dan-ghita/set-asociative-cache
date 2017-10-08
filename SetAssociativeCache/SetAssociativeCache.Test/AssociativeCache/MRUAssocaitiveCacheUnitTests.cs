
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test
{
    public class MRUAssocaitiveCacheUnitTests : AssociativeCacheUnitTests
    {
        public MRUAssocaitiveCacheUnitTests()
        {
            m_cache = new MRUAssociativeCache<string>(m_cacheSize);
        }


        [Fact]
        public void Add_CacheIsFull_RemovesMostRecentlyUsedElements()
        {
            FillCacheAndValidate();

            Assert.Equal("1", m_cache.Get(1));
            Assert.Equal("3", m_cache.Get(3));
            Assert.Equal("2", m_cache.Get(2));

            for (int i = m_cacheSize; i < 2 * m_cacheSize; ++i)
            {
                m_cache.Add(i, (i).ToString());

                if (i == m_cacheSize)
                    Assert.Equal(null, m_cache.Get(2));
                else
                    Assert.Equal(null, m_cache.Get(i - 1));

                Assert.Equal((i).ToString(), m_cache.Get(i));
                Assert.Equal(m_cacheSize, m_cache.Count);
            }
        }
    }
}
