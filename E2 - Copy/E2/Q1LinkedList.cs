using System;
using System.Collections;
using System.Collections.Generic;

namespace E2
{

    public class Q1LinkedList
    {
        public class Node
        {
            public Node(int key) { this.Key = key;  }
            public int Key;
            public Node Next = null;
            public Node Prev = null;
            public override string ToString() => ToString(10);

            public string ToString(int maxDepth)
            {
                return maxDepth == 1 || Next == null ?
                    $"{Key.ToString()}" + (Next != null ? "..." : string.Empty) :
                    $"{Key.ToString()} {Next.ToString(maxDepth - 1)}";
            }
        }

        private Node Head = null;
        private Node Tail = null;

        public void Insert(int key)
        {
            if (Head == null)
            {
                Head = Tail = new Node(key);
            }
            else
            {
                var newNode = new Node(key);
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public override string ToString() => Head.ToString();

        public void Reverse()
        {
            var p = this.Head;
            this.Head = this.Tail;
            ReverseRecursive(this.Head);

            // زحمت بکشید پیاده سازی کنید
            // اگر نیاز بود میتوانید متد اضافه کنید
        }
        public void ReverseRecursive(Node it)
        {
            if (it == null)
            {
                
                return;
            }
            
            var par = it.Next;
            it.Next = it.Prev;
            it.Prev = par;
            if (it.Next == null)
            {
                this.Tail = it;
            }
            ReverseRecursive(it.Next);

        }
        public void DeepReverse()
        {
            var it = this.Tail;
            this.Head = it;
            while (true)
            {
                var par = it.Next;
                it.Next = it.Prev;
                it.Prev = par;
                if (it.Next == null)
                    break;
                it = it.Next;
            }
            this.Tail = it;
            // زحمت بکشید پیاده سازی کنید
            // اگر نیاز بود میتوانید متد اضافه کنید
        }

        public IEnumerable<int> GetForwardEnumerator()
        {
            var it = this.Head;
            while (it != null)
            {
                yield return it.Key;
                it = it.Next;
            }
        }

        public IEnumerable<int> GetReverseEnumerator()
        {
            var it = this.Tail;
            while (it != null)
            {
                yield return it.Key;
                it = it.Prev;
            }
        }
    }
}