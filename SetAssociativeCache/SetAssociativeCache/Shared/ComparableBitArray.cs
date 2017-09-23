using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache.Shared
{
    public class ComparableBitArray : BitArray, IEquatable<BitArray>
    {
        public ComparableBitArray()
        {
        }

        public bool Equals(BitArray other)
        {
            if (this.Length != b2.Length)
                return false;

            for (int i = 0; i < Length; ++i)
                if (Get(i) != b2.Get(i))
                    return false;

            return true;
        }
    }
}
