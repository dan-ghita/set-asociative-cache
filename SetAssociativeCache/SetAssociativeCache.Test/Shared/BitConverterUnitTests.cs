using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SetAssociativeCache.Test.Shared
{
    public class BitConverterUnitTests
    {
        [Theory]
        [InlineData(1l, 1l, true)]
        [InlineData(1l, 2l, false)]
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

            Assert.Equal(areEqual, AreEqual(b1, b2));
        }

        [Fact]
        public void ObjectToBits_CustomTypes_ReturnsExpectedResult()
        {
            CacheNode<string> a = new CacheNode<string>(1, "1");
            CacheNode<string> b = new CacheNode<string>(2, "2");
            CacheNode<string> c = new CacheNode<string>(2, "2");

            Assert.Equal(false, AreEqual(BitConverter.ObjectToBits(a), BitConverter.ObjectToBits(b)));
            Assert.Equal(true, AreEqual(BitConverter.ObjectToBits(a), BitConverter.ObjectToBits(a)));
            Assert.Equal(true, AreEqual(BitConverter.ObjectToBits(b), BitConverter.ObjectToBits(c)));
        }


        private bool AreEqual(BitArray b1, BitArray b2)
        {
            if (b1.Length != b2.Length)
                return false;

            for (int i = 0; i < b1.Length; ++i)
                if (b1.Get(i) != b2.Get(i))
                    return false;

            return true;
        }
    }
}
