﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class BST
    {
        public class Node
        {
            public long Key { get; set; }
            protected Node _LeftChild;

            public Node Left
            {
                get => _LeftChild;
                set
                {
                    _LeftChild = value;
                    if (value != null)
                        _LeftChild.Parent = this;
                }
            }
            protected Node _RightChild;
            public Node Right
            {
                get => _RightChild;
                set
                {
                    _RightChild = value;
                    if (value != null)
                        _RightChild.Parent = this;
                }
            }
            public const string NullChar = "-";

            public Node Parent { get; set; }
            public bool IsLeftChild => Parent != null && ReferenceEquals(Parent.Left, this);
            public bool IsRightChild => Parent != null && ReferenceEquals(Parent.Right, this);

            public override string ToString()
            {
                try
                {
                    return ($"{Key}({(Left != null ? Left.ToString() : NullChar)}," +
                           $"{(Right != null ? Right.ToString() : NullChar)})")
                        .Replace("(-,-)", "")
                        .Replace("(-,-)", "")
                        .Trim();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            public Node(long key,
                Node leftChild = null,
                Node rightChild = null,
                Node parent = null)
            {
                Key = key;
                Left = leftChild;
                Right = rightChild;
                Parent = parent;
            }
        }

        public void Clear() => Root = null;

        public Node Root { get; protected set; }

        /// <summary>
        /// If DebugMode is on, the entire tree will be checked
        /// for parent-child consistence and making sure there are 
        /// no loops. It adds a huge performance cost, so only turn
        /// it on when you are trying to find a bug.
        /// You can add more calls to EnsureBSTConsistency where needed.
        /// </summary>
        public static bool DebugMode { get; set; } = false;

        public virtual Node Find(long key)
        {
            if (Root == null)
                return null;
            Stack<Node> nodes = new Stack<Node>();

            nodes.Push(this.Root);

            while (nodes.Count > 0)
            {

                var n = nodes.Pop();

                if (n.Key == key)
                    return n;

                if (n.Key>key)
                {
                    if (n.Left == null)
                        return n;
                    nodes.Push(n.Left);
                }
                else
                {
                    if (n.Right == null)
                        return n;
                    nodes.Push(n.Right);
                }
            }

            return null;
            
        }

        public static BST ParseBST(IEnumerable<long> preOrderList)
        {
            var Root = ParseBST(ref preOrderList);
            return new BST(Root);
        }

        public static Node ParseBST(ref IEnumerable<long> preOrderList)
        {
            if (!preOrderList.Any())
                return null;

            long nextNode = preOrderList.First();
            preOrderList = preOrderList.Skip(1);

            if (nextNode == -1)
                return null;

            Node n = new Node(nextNode);

            n.Left = ParseBST(ref preOrderList);
            n.Right = ParseBST(ref preOrderList);

            return n;
        }

        public BST(Node Root = null)
        {
            this.Root = Root;
        }

        public override string ToString()
            => Root?.ToString();

        public virtual void Insert(long key)
        {
            var p = Find(key);
            if(p == null)
            {
                this.Root = new Node(key);
                return;
            }
            Node n = new Node(key);
            n.Parent = p;
            if (p.Key > n.Key)
                p.Left = n;
            else
                p.Right = n;
            
        }

        public virtual void Delete(Node n)
        {
            Node next = Next(n);
            if (n == Root)
            {
                n.Key = next.Key;
                next.Parent.Left = next.Left;
            }
            else if (n.Right != null)
            {
                n.Key = next.Key;
                n.Right.Parent.Key = next.Key;
                if(n.Left != null )
                {
                    n.Left.Parent.Key = next.Key;
                }
                next.Parent.Right = next.Right;
            }
            else
            {
                if(next == null)
                {
                    n.Parent.Right = n.Left;
                }
                else
                {
                    n.Parent.Left = n.Left;
                }
            }
        }
        public virtual void Delete(long key) { }

        public Node Next(Node n)
        {
            Stack<Node> nodes = new Stack<Node>();

            if(n.Right != null)
            {
                nodes.Push(n.Right);
                while (nodes.Count > 0)
                {
                    var m = nodes.Pop();
                    if(m.Left == null)
                        return m;
                    
                    else
                        nodes.Push(m.Left);
                    
                }
            }
            else
            {
                nodes.Push(n);
                while (nodes.Count > 0)
                {
                    var m = nodes.Pop();
                    if (m.Key == this.Root.Key)
                        return null;
                    if (m.Key < m.Parent.Key)
                        
                        return m.Parent;

                    else
                        nodes.Push(m.Parent);
                }

            }

            return null;
        }
        public Node Next(long key) => Next(Find(key));

        public IEnumerable<Node> RangeSearch(long x, long y)
        {
            List<Node> nodes = new List<Node>();
            var n = Find(x);
            while(n.Key<=y)
            {
                if(n.Key>= x)
                {
                    nodes.Add(n);
                }
                n = this.Next(n);
            }
            return nodes;
        }

        public static bool EnsureBSTConsistency(BST.Node r)
        {
            if (r == null)
                return true;

            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(r);

            while (nodes.Count > 0)
            {

                var n = nodes.Dequeue();

                // Make sure left child points back to parent
                if (n.Left != null && !ReferenceEquals(n.Left.Parent, n))
                    return false;

                // Make sure right child points back to parent
                if (n.Right != null && !ReferenceEquals(n.Right.Parent, n))
                    return false;

                // Make sure no node is its own parent
                if (n.Parent != null && ReferenceEquals(n, n.Parent))
                    return false;

                if (n.Left != null)
                    nodes.Enqueue(n.Left);

                if (n.Right != null)
                    nodes.Enqueue(n.Right);

            }
            return true;
        }

        protected void UpdateParentWithNewNode(Node parent, Node n, Node newNode)
        {
            if (parent == null)
            {
                Root = newNode;
                if (Root != null) 
                    Root.Parent = null;

                return;
            }

            if (ReferenceEquals(parent.Left,n))
                parent.Left = newNode;
            else
                parent.Right = newNode;
        }

        protected Node RotateRight(Node x) => null;


        protected Node RotateLeft(Node y) => null;
    }
}
