using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximumGold : Processor
    {
        public MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long W, long[] goldBars)
        {
            List<long> goldBars2 = new List<long> { 0 };
            goldBars2.AddRange(goldBars);
            goldBars2 = goldBars2.Where(x => x <= W).ToList();
            goldBars2 = goldBars2.OrderBy(x => x).ToList();
            long[,] resultGold = new long[ goldBars2.Count + 1 , W+1];

            
            for (int i = 0; i < W+1; i++)
            {
                resultGold[ 0 ,i] = 0;
            }
            for (int i = 0; i < goldBars2.Count +1; i++)
            {
                resultGold[ i ,0] = 0;
            }
            long last ,predict;
            for (int i = 1; i < goldBars2.Count; i++)
            {
                for (int j = 1; j < W+1 ; j++)
                {
                    predict = long.MinValue;
                    last = resultGold[i-1, j];
                    if (j>=goldBars2[i])
                        predict = goldBars2[i] + resultGold[i-1 ,j-goldBars2[i]];
                    if(j < predict)
                    {
                        resultGold[i, j] = last;
                    }
                    else
                    {
                        resultGold[i, j] = Math.Max(last, predict);
                    }

                }
            }
            return resultGold[goldBars2.Count -1 , W];
        }
    }
}
