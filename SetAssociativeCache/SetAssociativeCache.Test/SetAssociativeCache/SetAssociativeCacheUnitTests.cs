using Moq;
using SetAssociativeCache.Test.Shared;
using System;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public abstract class SetAssociativeCacheUnitTests
    {
        public SetAssociativeCacheUnitTests()
        {
            RegisterCache();
        }

        [Fact]
        public void Add_PersistsElement()
        {
            IKeyType key = new KeyType();
            string value = "testValue";

            m_cache.Add(key, value);

            Assert.Equal(value, m_cache.Get(key));
        }

        [Fact]
        public void AddToOneWayCache_SameKey_ReplacesOldValue()
        {
            m_numberOfWays = 1;
            m_setCount = 1024;

            int setIndex = new Random().Next() % m_setCount;

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
        public void AddToOneWayCache_DifferentSetBits_DoesNotReplaceOldValue()
        {
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

        public abstract void RegisterCache();

        protected int m_setCount = 256;

        protected int m_numberOfWays = 4;

        protected ISetAssociativeCache<IKeyType, string> m_cache;
    }
}
