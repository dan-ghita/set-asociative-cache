using System.Collections;
using System.Collections.Generic;

namespace SetAssociativeCache
{
    public abstract class RUAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        protected IDictionary<BitArray, CacheNode<TValue>> nodePointer;
        protected CacheNode<TValue> root, tail;

        public RUAssociativeCache(int size)
        {
            m_cacheSize = size;
            nodePointer = new Dictionary<BitArray, CacheNode<TValue>>(new BitArrayComparer());

            root = new CacheNode<TValue>(new BitArray(0), default(TValue));
            tail = new CacheNode<TValue>(new BitArray(0), default(TValue));

            root.Next = tail;
            tail.Previous = root;
        }


        public void Add(BitArray tag, TValue value)
        {
            if (nodePointer.ContainsKey(tag))
            {
                MoveToFront(nodePointer[tag]);
            }
            else
            {
                CacheNode<TValue> newNode = new CacheNode<TValue>(tag, value);

                if(Size == m_cacheSize)
                    ReplaceRecentlyUsed(newNode);
                else
                    AddToFront(newNode);

                nodePointer.Add(tag, newNode);
            }
        }


        public TValue Get(BitArray tag)
        {
            if (nodePointer.ContainsKey(tag))
            {
                MoveToFront(nodePointer[tag]);
                return nodePointer[tag].Value.Data;
            }
            else
            {
                return default(TValue);
            }
        }


        private void MoveToFront(CacheNode<TValue> node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            AddToFront(node);
        }


        protected abstract void ReplaceRecentlyUsed(CacheNode<TValue> node);


        protected void AddToFront(CacheNode<TValue> node)
        {
            node.Next = root.Next;
            node.Previous = root;
            root.Next.Previous = node;
            root.Next = node;
        }


        public int Size => nodePointer.Count;


        private int m_cacheSize;
    }
}
