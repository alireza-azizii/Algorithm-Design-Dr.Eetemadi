using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class TreeHeight : Processor
    {
        public TreeHeight(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long nodeCount, long[] tree )
        {
            Queue<long> nodes = new Queue<long>();

            List<long>[] pathes = new List<long>[(int)nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                pathes[i] = new List<long>();
            }
            

            for (int i = 0; i < nodeCount; i++)
            {
                if (tree[i] == -1)
                {
                    nodes.Enqueue(i);
                }
                else
                {
                    pathes[(int)tree[i]].Add(i);
                }
            }

            long height = 0;

            while (true)
            {
                int count = nodes.Count;
                if (count == 0)
                {
                    return height;
                }
                height += 1;
                while(count > 0)
                {
                    long node = nodes.Peek();
                    nodes.Dequeue();
                    if(pathes[(int)node].Count != 0)
                    {
                        for (int i = 0; i < pathes[(int)node].Count; i++)
                        {
                            nodes.Enqueue(pathes[(int)node][i]);
                        }

                    }
                    count--;
                }
            }

           // return height;
        }

        
        
    }
    
}   
