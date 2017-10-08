namespace SetAssociativeCache
{
    public class MRUAssociativeCache<TValue> : RUAssociativeCache<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MRUAssociativeCache{TValue}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public MRUAssociativeCache(int size) : base(size) { }


        /// <summary>
        /// Replaces the recently used element.
        /// </summary>
        /// <param name="node">The node.</param>
        protected override void ReplaceRecentlyUsed(CacheNode<TValue> node)
        {
            nodePointer.Remove(root.Next.Value.Tag);

            root.Next.Next.Previous = root;
            root.Next = root.Next.Next;

            AddToFront(node);
        }
    }
}
