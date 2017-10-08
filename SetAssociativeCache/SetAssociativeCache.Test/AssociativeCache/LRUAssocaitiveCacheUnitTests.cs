using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test
{
    public class LRUAssocaitiveCacheUnitTests : AssociativeCacheUnitTests
    {
        public LRUAssocaitiveCacheUnitTests()
        {
            m_cache = new LRUAssociativeCache<string>(m_cacheSize);
        }


        [Fact]
        public void Add_CacheIsFull_RemovesLeastRecentlyUsedElements()
        {
            FillCacheAndValidate();

            Assert.Equal("1", m_cache.Get(1));
            Assert.Equal("3", m_cache.Get(3));
            Assert.Equal("1", m_cache.Get(1));

            int[] removalOrder = new[] { 0, 2, 4, 3, 1 };

            for (int i = 0; i < removalOrder.Length; ++i)
            {
                m_cache.Add(m_cacheSize + i, (m_cacheSize + i).ToString());
                Assert.Equal(null, m_cache.Get(removalOrder[i]));
                Assert.Equal((m_cacheSize + i).ToString(), m_cache.Get(m_cacheSize + i));
                Assert.Equal(m_cacheSize, m_cache.Count);
            }

            for (int i = m_cacheSize; i < 2 * m_cacheSize; ++i)
                Assert.Equal(i.ToString(), m_cache.Get(i));
        }
    }
}
