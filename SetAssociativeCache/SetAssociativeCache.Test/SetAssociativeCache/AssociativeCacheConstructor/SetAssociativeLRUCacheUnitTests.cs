using SetAssociativeCache.Test.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeLRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                (size) => new LRUAssociativeCache<string>(size));
        }

        [Fact]
        public void Add_CacheIsFull_ReplacesLeastRecentlyUsed()
        {
            int wayToBeSkipped = m_rand.Next() % m_numberOfWays;

            FillCacheAndValidate();
            UseCacheItems(wayToBeSkipped);
            
            List<KeyValuePair<IKeyType, string>> newValues = new List<KeyValuePair<IKeyType, string>>();

            for (int i = 0; i < m_setCount; ++i)
                newValues.Add(new KeyValuePair<IKeyType, string>(new KeyType(i - m_setCount), m_rand.Next().ToString()));

            newValues.ForEach(pair => m_cache.Add(pair.Key, pair.Value));

            // New items should be persisted
            newValues.ForEach(pair => Assert.Equal(pair.Value, m_cache.Get(pair.Key)));

            // LRU items should have been removed
            for (int i = 0; i < m_setCount; ++i)
                Assert.Equal(null, m_cache.Get(new KeyType(wayToBeSkipped * m_setCount + i)));

            // Cache should still be full
            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }

        private void UseCacheItems(int wayToBeSkipped)
        {
            for (int i = 0; i < m_setCount; ++i)
                for (int j = 0; j < m_numberOfWays; ++j)
                    if(j != wayToBeSkipped)
                        m_cache.Get(new KeyType(j * m_setCount + i));
        }
    }
}