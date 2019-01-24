using System;
using TestCommon;
using System.Collections.Generic;
namespace A11
{
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);
        public long[][] Solve(long[][] nodes)
        {
            long[][] result = new long[3][];

            Node[] myNodes = new Node[nodes.Length];

            for (int i = 0; i < myNodes.Length; i++)
            {
                myNodes[i] = new Node(nodes, i);
            }

            result[0] = InOrder(myNodes);
            result[1] = PreOrder(myNodes);
            result[2] = PostOrder(myNodes);
            return result;
        }

        public long[] PostOrder(Node[] nodes)
        {
            List<long> result = new List<long>();
            Stack<Node> S = new Stack<Node>();
            Node n = nodes[0];
            S.Push(n);
            Node prev = null;
            while(S.Count>0)
            {
                n = S.Peek();
              
                if((n.Left == null && n.Right == null)||((prev!=null)&&((n.Left != null && n.Left.Id == prev.Id) || (n.Right !=null && n.Right.Id == prev.Id))))
                {
                    S.Pop();
                    result.Add(n.Key);
                    prev = n;
                }
                else
                {
                    if (n.Right != null)
                    {
                        S.Push(nodes[n.Right.Id]);
                    }
                    if (n.Left != null)
                    {
                        S.Push(nodes[n.Left.Id]);
                    }
                }

            }
            return result.ToArray();

        }

        public long[] PreOrder(Node[] nodes)
        {
            List<long> result = new List<long>();
            Stack<Node> S = new Stack<Node>();
            Node n = nodes[0];
            S.Push(n);
            while(S.Count>0)
            {
                result.Add(n.Key);
                if (n.Right != null)
                {
                    S.Push(nodes[n.Right.Id]);
                }
                if (n.Left != null)
                {
                    S.Push(nodes[n.Left.Id]);
                }
                n = S.Pop();
            }
            return result.ToArray();
        }

        public long[] InOrder(Node[] nodes)
        {
            List<long> result = new List<long>();
            Stack<Node> S = new Stack<Node>();
            Node n = nodes[0];
            while (S.Count > 0 || n != null)
            {
                while (n != null)
                {
                    S.Push(n);
                    n =(n.Left != null)? nodes[n.Left.Id] :null;
                }

                n = S.Pop();
                result.Add(n.Key);

                n= (n.Right != null) ? nodes[n.Right.Id]: null;
               
                
            }
            return result.ToArray();
        }

        public class Node
        {
            public long Key { set; get; }
            public Node Left { set; get; }
            public Node Right { set; get; }
            public long Id { get; set; }

            public Node(long[][] nodes, long id)
            {
                this.Key = nodes[id][0];
                this.Id = id;
                if(nodes[id][1]!=-1) this.Left = new Node(nodes[id][1]);
                if(nodes[id][2]!=-1) this.Right = new Node(nodes[id][2] );
            }
            public Node(long id)
            {
                this.Id = id;
            }

        }
    }
}
