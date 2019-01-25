using System;
using TestCommon;
using System.Collections.Generic;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

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
                adjacencyList[edges[i][0]].Add(edges[i][1]);
            }




            Stack<long> route = new Stack<long>();
            long n;

            bool[] check = new bool[nodeCount + 1];
            bool[] inStack = new bool[nodeCount + 1];
            long checking = 0;
            bool hasAdjacency = false;

            route.Push(1);
            inStack[1] = true;
            while (checking < nodeCount)
            {
                while (route.Count > 0)
                {
                    n = route.Peek();
                    check[n] = true;
                    checking++;

                    hasAdjacency = false;

                    foreach (var neigh in adjacencyList[n])
                    {
                        if(inStack[neigh])
                        {
                            return 1;
                        }
                        if (!check[neigh])
                        {
                            hasAdjacency = true;
                            route.Push(neigh);
                            inStack[neigh] = true;
                            check[neigh] = true;

                        }
                        
                    }
                    if(!hasAdjacency)
                    {
                        route.Pop();
                        inStack[n] = false;
                    }
                }


                for (int i = 1; i < check.Length; i++)
                {
                    if (!check[i])
                    {
                        route.Push(i);
                        inStack[i] = true;
                        break;
                    }
                }
            }

            return 0;
        }
    }
}