using System;
using TestCommon;

namespace A11
{
    public class IsItBST : Processor
    {
        public IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);

        public Node[] Nodes { get; set; }

        public bool Solve(long[][] nodes)
        {

            Nodes = new Node[nodes.Length];

            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Node(nodes, i);
            }
            return IsBST(Nodes[0]);
        }


        public bool IsBST( Node n , long min = long.MinValue , long max = long.MaxValue)
        {
            if(n == null)
            {
                return true;
            }

            if(n.Key< min || n.Key>max)
            {
                return false;
            }

            return IsBST(n.Left!=null ? Nodes[n.Left.Id]:null ,min , n.Key -1 ) && IsBST( n.Right != null ?Nodes[n.Right.Id]:null , n.Key+1 , max);
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
                if (nodes[id][1] != -1) this.Left = new Node(nodes[id][1]);
                if (nodes[id][2] != -1) this.Right = new Node(nodes[id][2]);
            }
            public Node(long id)
            {
                this.Id = id;
            }

        }
    }   


}
