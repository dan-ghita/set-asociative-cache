using BinaryFormatter;
using System.Collections;

namespace SetAssociativeCache
{
    /// <summary>
    /// Bit converter
    /// </summary>
    public static class BitConverter
    {
        /// <summary>
        /// Convert object to bits.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns></returns>
        public static BitArray ObjectToBits(object obj)
        {
            return new BitArray(ObjectToByteArray(obj));
        }


        /// <summary>
        /// Convert object to byte array.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray(object obj)
        {
            BinaryConverter bc = new BinaryConverter();
            return bc.Serialize(obj);
        }

        public static int ConvertToInt(BitArray bits)
        {
            int[] array = new int[1];
            ((ICollection)bits).CopyTo(array, 0);

            return array[0];
        }


        /// <summary>
        /// Determines whether the other object is equal.
        /// </summary>
        /// <param name="b1">This object</param>
        /// <param name="b2">Object to compare with</param>
        /// <returns>
        ///   <c>true</c> if the other object is equal; otherwise, <c>false</c>.
        /// </returns>
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
