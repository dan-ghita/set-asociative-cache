using BinaryFormatter;
using System.Collections;

namespace SetAssociativeCache
{
    public class BitConverter
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
    }
}
