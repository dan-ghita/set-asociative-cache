namespace SetAssociativeCache
{
    public class LRUAssociativeCache<TValue> : RUAssociativeCache<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LRUAssociativeCache{TValue}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public LRUAssociativeCache(int size) : base(size) { }


        /// <summary>
        /// Replaces the recently used element.
        /// </summary>
        /// <param name="node">The node.</param>
        protected override void ReplaceRecentlyUsed(CacheNode<TValue> node)
        {
            nodePointer.Remove(tail.Previous.Value.Tag);

            tail.Previous.Previous.Next = tail;
            tail.Previous = tail.Previous.Previous;

            AddToFront(node);
        }
    }
}
