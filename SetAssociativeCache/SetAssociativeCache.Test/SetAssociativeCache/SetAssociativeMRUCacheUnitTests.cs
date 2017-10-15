using SetAssociativeCache.Test.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeMRUCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                (size) => new MRUAssociativeCache<string>(size));
        }

        [Fact]
        public void Add_CacheIsFull_ReplacesMostRecentlyUsed()
        {
            int wayToBeUsed = m_rand.Next() % m_numberOfWays;

            FillCacheAndValidate();
            UseCacheItems(wayToBeUsed);

            List<KeyValuePair<IKeyType, string>> newValues = new List<KeyValuePair<IKeyType, string>>();

            for (int i = 0; i < m_setCount; ++i)
                newValues.Add(new KeyValuePair<IKeyType, string>(new KeyType(i - m_setCount), m_rand.Next().ToString()));

            newValues.ForEach(pair => m_cache.Add(pair.Key, pair.Value));

            // New items should be persisted
            newValues.ForEach(pair => Assert.Equal(pair.Value, m_cache.Get(pair.Key)));

            // MRU items should have been removed
            for (int i = 0; i < m_setCount; ++i)
                Assert.Equal(null, m_cache.Get(new KeyType(wayToBeUsed * m_setCount + i)));

            // Cache should still be full
            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }

        private void UseCacheItems(int wayToBeUsed)
        {
            for (int i = 0; i < m_setCount; ++i)
                m_cache.Get(new KeyType(wayToBeUsed * m_setCount + i));
        }
    }
}