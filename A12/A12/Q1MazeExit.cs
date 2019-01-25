using System;
using TestCommon;
using System.Collections.Generic;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
           

            List<long>[] adjacencyList = new List<long>[nodeCount+1];
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new List<long>();
            }


            for (int i = 0; i < edges.Length; i++)
            {
                adjacencyList[edges[i][1]].Add(edges[i][0]);
                adjacencyList[edges[i][0]].Add(edges[i][1]);
            }

            Stack<long> rout = new Stack<long>();

            rout.Push(StartNode);

            long n;

            bool[] check = new bool[nodeCount+1];

            while(rout.Count > 0)
            {
                n = rout.Pop();
                check[n] = true;
                if(n == EndNode)
                {
                    return 1;
                }

                foreach (var neigh in adjacencyList[n])
                {
                    if(!check[neigh])
                        rout.Push(neigh);
                }


            }

            return 0;
        }    
     }
}
