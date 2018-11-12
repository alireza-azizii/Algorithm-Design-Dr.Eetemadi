using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class PrimitiveCalculator: Processor
    {
        public PrimitiveCalculator(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);

        public long[] Solve(long n)
        {
            List<List<long> > minimumOperation = new List<List<long>>() { new List<long> { 0} , new List<long> { 1 }, new List<long> { 1, 2 }  , new List<long> { 1 , 3}  };
            long minLength;
            long minIndex;
            List<long> newList;
            for (int i = 4; i <= n ; i++)
            {
                newList = new List<long>();
                minLength = long.MaxValue;
                minIndex = 0; 
                if(minLength > minimumOperation[(int)i/3].Count && i % 3 == 0)
                {
                    minIndex = i / 3;
                    minLength = minimumOperation[(int)i / 3].Count;
                }
                if(minLength > minimumOperation[(int)i/2].Count && i%2 == 0)
                {
                    minIndex = i / 2;
                    minLength = minimumOperation[(int)i / 2].Count;

                }
                if(minLength > minimumOperation[i - 1].Count)
                {
                    minIndex = i - 1;
                    minLength = minimumOperation[i - 1].Count;
                }

                newList.AddRange(minimumOperation[(int)minIndex]);
                newList.Add(i);

                minimumOperation.Add(newList);
                
            }
            return minimumOperation[(int)n].ToArray();
        }

        public static long MinOf3(long a ,long b ,long c)
        {
            if (a < b && a < c)
                return a;
            else if (b < a && b < c)
                return b;
            return c;
        }
    }
}
