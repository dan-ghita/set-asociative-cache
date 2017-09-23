using BinaryFormatter;
using System.Collections;

namespace SetAssociativeCache
{
    public static class BitConverter
    {
        public static BitArray ObjectToBits(object obj)
        {
            return new BitArray(ObjectToByteArray(obj));
        }


        public static byte[] ObjectToByteArray(object obj)
        {
            BinaryConverter bc = new BinaryConverter();
            return bc.Serialize(obj);
        }


        public static bool IsEqual(this BitArray b1, BitArray b2)
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
