﻿using System;
using System.IO;
using System.Linq;
using TestCommon;
using System.Collections.Generic;

namespace A12
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
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

                    foreach (var neigh in adjacencyList[n])
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
            return topolo.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        /// <summary>
        /// کد شما با متد زیر راست آزمایی میشود
        /// این کد نباید تغییر کند
        /// داده آزمایشی فقط یک جواب درست است
        /// تنها جواب درست نیست
        /// </summary>
        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
