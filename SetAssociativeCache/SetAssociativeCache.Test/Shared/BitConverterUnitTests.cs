using SetAssociativeCache.Shared;
using System.Collections;
using Xunit;

namespace SetAssociativeCache.Test.Shared
{
    public class BitConverterUnitTests
    {
        public BitConverterUnitTests()
        {
            BitConverter = new BitConverter();
        }

        [Theory]
        [InlineData(1L, 1L, true)]
        [InlineData(1L, 2L, false)]
        [InlineData("a", "a", true)]
        [InlineData("a", "b", false)]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(1, 1, true)]
        [InlineData(1, "1", false)]
        [InlineData('1', "1", false)]
        public void ObjectToBits_PrimitiveTypes_ReturnsExpectedResult<T1, T2>(T1 a, T2 b, bool areEqual)
        {   
            BitArray b1 = BitConverter.ObjectToBits(a);
            BitArray b2 = BitConverter.ObjectToBits(b);

            Assert.Equal(areEqual, b1.IsEqual(b2));
        }

        [Fact]
        public void ObjectToBits_CustomTypes_ReturnsExpectedResult()
        {
            CacheNode<string> a = new CacheNode<string>(new BitArray(1), "1");
            CacheNode<string> b = new CacheNode<string>(new BitArray(2), "2");
            CacheNode<string> c = new CacheNode<string>(new BitArray(2), "2");

            Assert.Equal(false, BitConverter.ObjectToBits(a).IsEqual(BitConverter.ObjectToBits(b)));
            Assert.Equal(true, BitConverter.ObjectToBits(a).IsEqual(BitConverter.ObjectToBits(a)));
            Assert.Equal(true, BitConverter.ObjectToBits(b).IsEqual(BitConverter.ObjectToBits(c)));
        }

        [Theory]
        [InlineData(new[] { true, true, false, false }, 3)]
        [InlineData(new[] { true, true, false, true }, 11)]
        [InlineData(new[] { false, false, true, true }, 12)]
        public void ConvertToInt_ReturnsExpectedValue(bool[] binaryNuber, int decimalNumber)
            => Assert.Equal(decimalNumber, BitConverter.ConvertToInt(new BitArray(binaryNuber)));

        public IBitConverter BitConverter { get; set; }
    }
}
