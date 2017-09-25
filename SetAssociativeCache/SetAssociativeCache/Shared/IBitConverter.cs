using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache.Shared
{
    public interface IBitConverter
    {
        /// <summary>
        /// Convert object to bits.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns></returns>
        BitArray ObjectToBits(object obj);

        int ConvertToInt(BitArray bits);
    }
}
