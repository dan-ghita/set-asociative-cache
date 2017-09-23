using System;
using System.Collections.Generic;
using System.Text;

namespace SetAssociativeCache
{
    public abstract class RUAssociativeCache<TValue> : IAssociativeCache<TValue>
    {
        protected IDictionary<int, CacheNode<TValue>> nodePointer;
        protected CacheNode<TValue> root, tail;

        public RUAssociativeCache(int size)
        {
            m_cacheSize = size;
            nodePointer = new Dictionary<int, CacheNode<TValue>>();

            root = new CacheNode<TValue>(-1, default(TValue));
            tail = new CacheNode<TValue>(-1, default(TValue));

            root.Next = tail;
            tail.Previous = root;
        }


        public void Add(int tag, TValue value)
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


        public TValue Get(int tag)
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
