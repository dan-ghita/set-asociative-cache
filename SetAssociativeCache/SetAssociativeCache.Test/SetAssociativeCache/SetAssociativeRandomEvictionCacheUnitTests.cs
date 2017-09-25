using Moq;
using SetAssociativeCache.Shared;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.SetAssociativeCache
{
    public class SetAssociativeRandomEvictionCacheUnitTests : SetAssociativeCacheUnitTests
    {
        public SetAssociativeRandomEvictionCacheUnitTests()
        {
            m_cache = new SetAssociativeCache<string, string>(m_cacheSizeInKb, m_numberOfWays,
                (size) => new RandomEvictionAssociativeCache<string>(size), m_bitConverter);
        }

        [Fact]
        public void AddToOneWayCache_SimilarSetBits_ReplacesOldValue()
        {
            Mock<IBitConverter> bitConverterMock = new Mock<IBitConverter>();
            bitConverterMock.Setup(bc => bc.ObjectToBits(1)).Returns(new BitArray(new[] { true, false, false, true, false }));
            bitConverterMock.Setup(bc => bc.ObjectToBits(2)).Returns(new BitArray(new[] { true, false, false, true, true }));

            // 1 * 1024 / 64 = 16 -> first 4 bit for set index
            ISetAssociativeCache<int, string> m_cache = new SetAssociativeCache<int, string>(1, 1,
                (size) => new RandomEvictionAssociativeCache<string>(size), bitConverterMock.Object);

            int key1 = 1;
            int key2 = 2;
            string value1 = "value1";
            string value2 = "value2";

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
            Mock<IBitConverter> bitConverterMock = new Mock<IBitConverter>();
            bitConverterMock.Setup(bc => bc.ObjectToBits(1)).Returns(new BitArray(new[] { true, false, false, true, false }));
            bitConverterMock.Setup(bc => bc.ObjectToBits(2)).Returns(new BitArray(new[] { true, true, false, true, true }));
            bitConverterMock.Setup(bc => bc.ConvertToInt(It.IsAny<BitArray>())).Returns<BitArray>(b => (new BitConverter()).ConvertToInt(b));

            // 1 * 1024 / 64 = 16 -> first 4 bit for set index
            ISetAssociativeCache<int, string> m_cache = new SetAssociativeCache<int, string>(1, 1,
                (size) => new RandomEvictionAssociativeCache<string>(size), bitConverterMock.Object);

            int key1 = 1;
            int key2 = 2;
            string value1 = "value1";
            string value2 = "value2";

            m_cache.Add(key1, value1);
            m_cache.Add(key2, value2);

            Assert.Equal(2, m_cache.Size);
            Assert.Equal(value1, m_cache.Get(key1));
            Assert.Equal(value2, m_cache.Get(key2));
        }
    }
}