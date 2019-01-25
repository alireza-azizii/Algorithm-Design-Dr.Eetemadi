using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjacencyList = new List<long>[nodeCount + 1];
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; i++)
            {
                adjacencyList[edges[i][1]].Add(edges[i][0]);
                adjacencyList[edges[i][0]].Add(edges[i][1]);
            }



            Stack<long> route = new Stack<long>();
            route.Push(1);

            long n;

            bool[] check = new bool[nodeCount + 1];
            long checking = 0;
            long component = 0;
            while (checking <nodeCount)
            {
                while(route.Count>0)
                {
                    n = route.Pop();
                    check[n] = true;
                    checking++;
                    foreach (var neigh in adjacencyList[n])
                    {
                        if (!check[neigh] )
                        {
                            route.Push(neigh);
                            check[neigh] = true;
                        }
                    }
                }

                component++;

                for (int i = 1; i < check.Length; i++)
                {
                    if(!check[i])
                    {
                        route.Push(i);
                        break;
                    }
                }
            }

            return component;
        }
    }
}
