using SetAssociativeCache.Test.Shared;
using System;
using System.Collections.Generic;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public abstract class SetAssociativeCacheUnitTests
    {
        public SetAssociativeCacheUnitTests()
        {
            m_rand = new Random();
            m_setCount = m_rand.Next(1, m_maxNumberOfSets);
            m_numberOfWays = m_rand.Next(1, m_maxNumberOfWays);

            RegisterCache();
        }

        public abstract void RegisterCache();

        [Fact]
        public void Add_PersistsElement()
        {
            IKeyType key = new KeyType();
            string value = "testValue";

            m_cache.Add(key, value);

            Assert.Equal(value, m_cache.Get(key));
        }

        [Fact]
        public void AddToOneWayCache_KeyFallsInSameSet_ReplacesOldValue()
        {
            m_numberOfWays = 1;
            m_setCount = 1024;

            int setIndex = m_rand.Next() % m_setCount;

            IKeyType key1 = new KeyType(setIndex);
            IKeyType key2 = new KeyType(m_setCount + setIndex);

            string value1 = "value1";
            string value2 = "value2";

            RegisterCache();

            m_cache.Add(key1, value1);
            Assert.Equal(value1, m_cache.Get(key1));

            m_cache.Add(key2, value2);
            Assert.Equal(value2, m_cache.Get(key2));

            Assert.Equal(1, m_cache.Count);
            Assert.Equal(null, m_cache.Get(key1));
        }

        [Fact]
        public void AddToOneWayCache_KeyFallsInDifferentSet_DoesNotReplaceOldValue()
        {
            m_numberOfWays = 1;
            m_setCount = 1024;

            IKeyType key1 = new KeyType(1);
            IKeyType key2 = new KeyType(2);

            string value1 = "value1";
            string value2 = "value2";

            m_cache.Add(key1, value1);
            m_cache.Add(key2, value2);

            Assert.Equal(2, m_cache.Count);
            Assert.Equal(value1, m_cache.Get(key1));
            Assert.Equal(value2, m_cache.Get(key2));
        }

        [Fact]
        public void Clear_RemovesAllEntries_CacheIsReusable()
        {
            FillCacheAndValidate();

            m_cache.Clear();

            Assert.Equal(0, m_cache.Count);

            FillCacheAndValidate();
        }

        [Fact]
        public void Add_CacheIsFull_PersistsNewElements()
        {
            FillCacheAndValidate();

            List<KeyValuePair<IKeyType, string>> newValues = new List<KeyValuePair<IKeyType, string>>();

            for (int i = 0; i < m_setCount; ++i)
                newValues.Add(new KeyValuePair<IKeyType, string>(new KeyType(i - m_setCount), m_rand.Next().ToString()));

            newValues.ForEach(pair => m_cache.Add(pair.Key, pair.Value));

            newValues.ForEach(pair => Assert.Equal(m_cache.Get(pair.Key), pair.Value));

            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }

        [Fact]
        public void Add_CustomNoEvictionReplacementStrategy_DoesNotEvictWhenFull()
        {
            m_cache = new SetAssociativeCache<IKeyType, string>(m_setCount, m_numberOfWays,
                (size) => new NoEvictionAssociativeCache<string>(size));

            FillCacheAndValidate();

            List<KeyValuePair<IKeyType, string>> newValues = new List<KeyValuePair<IKeyType, string>>();

            for (int i = 0; i < m_setCount; ++i)
                newValues.Add(new KeyValuePair<IKeyType, string>(new KeyType(i - m_setCount), m_rand.Next().ToString()));

            newValues.ForEach(pair => m_cache.Add(pair.Key, pair.Value));

            newValues.ForEach(pair => Assert.Equal(m_cache.Get(pair.Key), null));

            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }

        protected void FillCacheAndValidate()
        {
            for (int i = 0; i < m_setCount; ++i)
                for (int j = 0; j < m_numberOfWays; ++j)
                    m_cache.Add(new KeyType(j * m_setCount + i), m_rand.Next().ToString());

            Assert.Equal(m_cache.Count, m_setCount * m_numberOfWays);
        }

        protected int m_setCount;

        protected int m_numberOfWays = 4;

        protected ISetAssociativeCache<IKeyType, string> m_cache;

        protected Random m_rand;

        private const int m_maxNumberOfSets = 1 << 10;

        private const int m_maxNumberOfWays = 1 << 10;
    }
}
