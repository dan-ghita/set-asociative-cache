using System.Collections;
using System.Collections.Generic;

namespace SetAssociativeCache.Shared
{
    public class BitArrayComparer : IEqualityComparer<BitArray>
    {
        public bool Equals(BitArray x, BitArray y) => x.IsEqual(y);

        public int GetHashCode(BitArray obj) => base.GetHashCode();
    }
}
