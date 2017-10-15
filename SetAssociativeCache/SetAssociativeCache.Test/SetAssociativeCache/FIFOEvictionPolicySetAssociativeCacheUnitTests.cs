using SetAssociativeCache.Test.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class FIFOEvictionPolicySetAssociativeCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public override void RegisterCache()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                () => new FIFOEvictionPolicy<string>());
        }

        [Fact]
        public void Add_CacheIsFull_ReplacesLeastRecentlyUsed()
        {
            FillCacheAndValidate();

            for (int i = 0; i <= Math.Min(10, m_numberOfWays); ++i)
                InsertNewValuesAndValidate(i);
        }

        private void InsertNewValuesAndValidate(int offset)
        {
            List<KeyValuePair<IKeyType, string>> newValues = new List<KeyValuePair<IKeyType, string>>();

            for (int i = 0; i < m_setCount; ++i)
                newValues.Add(new KeyValuePair<IKeyType, string>(new KeyType(i - (offset + 1) * m_setCount), m_rand.Next().ToString()));

            newValues.ForEach(pair => m_cache.Add(pair.Key, pair.Value));

            // New items should be persisted
            newValues.ForEach(pair => Assert.Equal(pair.Value, m_cache.Get(pair.Key)));

            // First items should have been removed
            for (int i = 0; i < m_setCount; ++i)
                Assert.Equal(null, m_cache.Get(new KeyType(offset * m_setCount + i)));

            // Cache should still be full
            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }
    }
}