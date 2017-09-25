using BinaryFormatter;
using System.Collections;

namespace SetAssociativeCache.Shared
{
    /// <summary>
    /// Bit converter
    /// </summary>
    public class BitConverter : IBitConverter
    {
        /// <summary>
        /// Convert object to bits.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns></returns>
        public BitArray ObjectToBits(object obj)
        {
            return new BitArray(ObjectToByteArray(obj));
		}


		/// <summary>
		/// Convert object to byte array.
		/// </summary>
		/// <param name="obj">Object to be converted.</param>
		/// <returns></returns>
		public byte[] ObjectToByteArray(object obj)
		{
			BinaryConverter bc = new BinaryConverter();
			return bc.Serialize(obj);
		}

		public int ConvertToInt(BitArray bits)
        {
            int[] array = new int[1];
            ((ICollection)bits).CopyTo(array, 0);

            return array[0];
        }
    }
}
