using System;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache.Test
{
    public class RandomEvictionAssocaitiveCacheUnitTests : AssociativeCacheUnitTests
    {
        public RandomEvictionAssocaitiveCacheUnitTests()
        {
            m_cache = new RandomEvictionAssociativeCache<string>(m_cacheSize);
        }
    }
}
