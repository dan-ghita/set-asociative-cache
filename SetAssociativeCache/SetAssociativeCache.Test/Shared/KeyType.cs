using System;

namespace SetAssociativeCache.Test.Shared
{
    public class KeyType : IKeyType, IEquatable<KeyType>
    {
        int m_hashCode;

        public KeyType(int? hashCode = null) => m_hashCode = hashCode ?? base.GetHashCode();

        public bool Equals(KeyType other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode() => m_hashCode;
    }
}
