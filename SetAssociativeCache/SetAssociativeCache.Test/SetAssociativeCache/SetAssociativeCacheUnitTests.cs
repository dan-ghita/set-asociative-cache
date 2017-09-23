using System;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public abstract class SetAssociativeCacheUnitTests
    {
        [Fact]
        public void Add_PersistsElement()
        {
            string key = "testKey";
            string value = "testValue";

            m_cache.Add(key, value);

            Assert.Equal(value, m_cache.Get(key));
        }
        

        protected int m_cacheSizeInKb = 64;

        protected int m_numberOfWays = 2;

        protected ISetAssociativeCache<string, string> m_cache;
    }
}