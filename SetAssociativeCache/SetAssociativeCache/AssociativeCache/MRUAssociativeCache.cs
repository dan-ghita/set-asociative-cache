using System;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache
{
    public class MRUAssociativeCache<TValue> : RUAssociativeCache<TValue>
    {
        public MRUAssociativeCache(int size) : base(size) { }


        protected override void ReplaceRecentlyUsed(CacheNode<TValue> node)
        {
            nodePointer.Remove(root.Next.Value.Tag);

            root.Next.Next.Previous = root;
            root.Next = root.Next.Next;

            AddToFront(node);
        }
    }
}
