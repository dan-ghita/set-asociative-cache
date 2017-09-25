using Moq;
using SetAssociativeCache.Shared;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public abstract class SetAssociativeCacheUnitTests
    {
        public SetAssociativeCacheUnitTests()
        {
            m_bitConverter = new BitConverter();
        }

        [Fact]
        public void Add_PersistsElement()
        {
            string key = "testKey";
            string value = "testValue";

            m_cache.Add(key, value);

            Assert.Equal(value, m_cache.Get(key));
        }

        [Fact]
        public void AddToOneWayCache_SimilarSetBits_ReplacesOldValue()
        {
            string key1 = "1";
            string key2 = "2";
            string value1 = "value1";
            string value2 = "value2";

            Mock<IBitConverter> bitConverterMock = new Mock<IBitConverter>();
            bitConverterMock.Setup(bc => bc.ObjectToBits(key1)).Returns(new BitArray(new[] { true, false, false, true, false }));
            bitConverterMock.Setup(bc => bc.ObjectToBits(key2)).Returns(new BitArray(new[] { true, false, false, true, true }));

            RegisterCacheWithMock(bitConverterMock);

            m_cache.Add(key1, value1);
            Assert.Equal(value1, m_cache.Get(key1));

            m_cache.Add(key2, value2);
            Assert.Equal(value2, m_cache.Get(key2));

            Assert.Equal(1, m_cache.Size);
            Assert.Equal(null, m_cache.Get(key1));
        }

        [Fact]
        public void AddToOneWayCache_DifferentSetBits_DoesNotReplaceOldValue()
        {
            string key1 = "1";
            string key2 = "2";
            string value1 = "value1";
            string value2 = "value2";

            Mock<IBitConverter> bitConverterMock = new Mock<IBitConverter>();
            bitConverterMock.Setup(bc => bc.ObjectToBits(key1)).Returns(new BitArray(new[] { true, false, false, true, false }));
            bitConverterMock.Setup(bc => bc.ObjectToBits(key2)).Returns(new BitArray(new[] { true, true, false, true, true }));
            bitConverterMock.Setup(bc => bc.ConvertToInt(It.IsAny<BitArray>())).Returns<BitArray>(b => (new BitConverter()).ConvertToInt(b));

            RegisterCacheWithMock(bitConverterMock);

            m_cache.Add(key1, value1);
            m_cache.Add(key2, value2);

            Assert.Equal(2, m_cache.Size);
            Assert.Equal(value1, m_cache.Get(key1));
            Assert.Equal(value2, m_cache.Get(key2));
        }

        public abstract void RegisterCacheWithMock(Mock<IBitConverter> bitConverter);

        protected int m_cacheSizeInKb = 64;

        protected int m_numberOfWays = 2;

        protected ISetAssociativeCache<string, string> m_cache;

        protected IBitConverter m_bitConverter;
    }
}
