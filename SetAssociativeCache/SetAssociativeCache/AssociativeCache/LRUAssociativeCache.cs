using System;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache
{
    public class LRUAssociativeCache<TValue> : RUAssociativeCache<TValue>
    {
        public LRUAssociativeCache(int size) : base(size) { }


        protected override void ReplaceRecentlyUsed(CacheNode<TValue> node)
        {
            nodePointer.Remove(tail.Previous.Value.Tag);

            tail.Previous.Previous.Next = tail;
            tail.Previous = tail.Previous.Previous;

            AddToFront(node);
        }
    }
}
