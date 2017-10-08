using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache.Shared
{
	public static class BitArrayExtensions
	{
		/// <summary>
		/// Determines whether the other object is equal.
		/// </summary>
		/// <param name="b1">This object</param>
		/// <param name="b2">Object to compare with</param>
		/// <returns>
		///   <c>true</c> if the other object is equal; otherwise, <c>false</c>.
		/// </returns>
		public static bool Equals(this BitArray b1, BitArray b2)
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
