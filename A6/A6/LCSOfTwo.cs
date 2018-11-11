using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class LCSOfTwo: Processor
    {
        public LCSOfTwo(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2)
        {
            long[,] dbResult = new long[seq1.Length+1, seq2.Length+1];

            for (int i = 1; i <= seq1.Length; i++)
            {
                for (int j = 1; j <= seq2.Length; j++)
                {
                    if(seq1[i-1] == seq2[j-1])
                    {
                        dbResult[i, j] = dbResult[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dbResult[i, j] = Math.Max(dbResult[i - 1, j], dbResult[i, j - 1]);
                    }
                }
            }

            return dbResult[seq1.Length , seq2.Length];
        }
    }
}
