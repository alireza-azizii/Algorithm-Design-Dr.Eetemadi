using System;
using TestCommon;
using System.Collections.Generic;

namespace A12
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            List<long>[] adjacencyListMain = new List<long>[nodeCount + 1];
            List<long>[] adjacencyListReverse = new List<long>[nodeCount + 1];

            for (int i = 0; i < adjacencyListMain.Length; i++)
            {
                adjacencyListMain[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; i++)
            {
                adjacencyListMain[edges[i][0]].Add(edges[i][1]);
            }


            for (int i = 0; i < adjacencyListReverse.Length; i++)
            {
                adjacencyListReverse[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; i++)
            {
                adjacencyListReverse[edges[i][1]].Add(edges[i][0]);
            }

            //dfs on main graph


            Stack<long> route = new Stack<long>();
            long n;
            List<long> topolo = new List<long>();
            bool[] check = new bool[nodeCount + 1];
            long checking = 1;
            bool hasAdjacency = false;

            route.Push(1);
            check[1] = true;

            while (true)
            {
                while (route.Count > 0)
                {

                    n = route.Peek();

                    hasAdjacency = false;

                    foreach (var neigh in adjacencyListMain[n])
                    {

                        if (!check[neigh])
                        {
                            checking++;
                            check[neigh] = true;
                            hasAdjacency = true;
                            route.Push(neigh);
                            break;
                        }

                    }
                    if (!hasAdjacency)
                    {
                        route.Pop();
                        topolo.Add(n);
                    }
                }
                if (checking == nodeCount)
                    break;

                for (int i = 1; i < check.Length; i++)
                {
                    if (!check[i])
                    {
                        route.Push(i);
                        checking++;
                        check[i] = true;
                        break;
                    }
                }
            }

            topolo.Reverse();

            route = new Stack<long>();
            long m;
            check = new bool[nodeCount + 1];
            checking = 1;
            hasAdjacency = false;

            route.Push(topolo[0]);
            check[topolo[0]] = true;
            long component = 1;

            while (true)
            {
                while (route.Count > 0)
                {

                    n = route.Peek();

                    hasAdjacency = false;

                    foreach (var neigh in adjacencyListReverse[n])
                    {

                        if (!check[neigh])
                        {
                            checking++;
                            check[neigh] = true;
                            hasAdjacency = true;
                            route.Push(neigh);
                            break;
                        }

                    }
                    if (!hasAdjacency)
                    {
                        route.Pop();
                        topolo.Add(n);
                    }
                }
                if (checking == nodeCount)
                    break;

                component++;

                foreach(var item in topolo)
                {
                    if (!check[item])
                    {
                        route.Push(item);
                        checking++;
                        check[item] = true;
                        break;
                    }
                }
            }

            return component;
        }
    }
}
