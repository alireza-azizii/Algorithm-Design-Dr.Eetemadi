using System;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q4TreeDiameter
    {
        /// <summary>
        /// ریشه همیشه نود صفر است.
        ///توی این آرایه در مکان صفر لیستی از بچه های ریشه موجودند.
        ///و در مکانه آی از این آرایه لیست بچه های نود آیم هستند
        ///اگر لیست خالی بود، بچه ندارد
        /// </summary>
        public List<int>[] Nodes;

        public Q4TreeDiameter(int nodeCount, int seed = 0)
        {
            Nodes = GenerateRandomTree(size: nodeCount, seed: seed);
            
        }

        public int TreeHeight()
        {
            Stack<List<int>> tree = new Stack<List<int>>();
            tree.Push(this.Nodes[0]);
            List<int> n;
            int h = 0;
            int height = int.MinValue;
            while(tree.Count != 0)
            {
                
                n = tree.Pop();

                if(n.Count == 0)
                {
                    if (h > height)
                        height = h;
                    h--;
                    continue;
                }
                else
                {
                    foreach (var item in n)
                    {
                        tree.Push(Nodes[item]);
                    }
                }
                h++;

            }
            return height;
        }

        public int TreeHeightFromNode(int node)
        {
            Stack<int> tree = new Stack<int>();
            bool[] check = new bool[Nodes.Length];
            check.Select(x => x = false);
            tree.Push(node);
            check[node] = true;
            int n;
            int h = 0;
            int height = int.MinValue;

            int[] parents = new int[Nodes.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }
            for (int i = 0; i < Nodes.Length; i++)
            {
                for (int j = 0; j < Nodes.Length; j++)
                {
                    if(Nodes[j].Contains(i))
                    {
                        parents[i] = j;
                        break;
                    }
                }
            }
            while (tree.Count != 0)
            {

                n = tree.Pop();
                check[n] = true;
                if (Nodes[n].Count == 0  &&  check[parents[n]])
                {
                    if (h > height)
                        height = h;
                    h--;
                    continue;
                }
                else
                {
                    foreach (var item in Nodes[n])
                    {
                        if(!check[item])
                            tree.Push(item);
                    }
                    if (  n != 0 && !check[parents[n]])
                        tree.Push(parents[n]);
                }
                h++;
            }
            return height;
        }

        public int TreeDiameterN2()
        {
            int maximum = int.MinValue;
            for (int i = 0; i < Nodes.Length; i++)
            {
                int h = this.TreeHeightFromNode(i);
                if (h > maximum)
                    maximum = h;
            }
            return maximum;
        }

        public int TreeDiameterN()
        {
            return 0;
        }
        

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();
            
            List<int> orphans = 
                new List<int>(Enumerable.Range(1, size-1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i=0; i< Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count-1);
                    int orphan = orphans[orphanIdx];
                    orphans.RemoveAt(orphanIdx);
                    nodes[parent].Add(orphan);
                    parentsQ.Enqueue(orphan);
                }
            }
            return nodes;
        }
    }
}