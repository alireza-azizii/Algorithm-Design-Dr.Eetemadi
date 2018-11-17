using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class PartitioningSouvenirs : Processor
    {
        public PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            if (souvenirsCount < 3)
                return 0;
            long sumOfsouvenirs = souvenirs.Sum();

            if (sumOfsouvenirs %  3 != 0)
            {
                return 0;
            }

            bool flag = SubProblem(souvenirs, sumOfsouvenirs / 3, sumOfsouvenirs / 3, sumOfsouvenirs / 3 , souvenirsCount-1);

            return flag == true ? 1 : 0;
        }

        private bool SubProblem(long[] souvenirs, long A, long B, long C , long n)
        {
            if (A == 0 && B == 0 && C == 0)
                return true;

            if (n < 0)
                return false;
            bool a = false;
            if (A >= souvenirs[n])
                a = SubProblem(souvenirs, A - souvenirs[n], B, C, n - 1);
            bool b = false;
            if (B >= souvenirs[n]  && !a)
                b = SubProblem(souvenirs, A, B - souvenirs[n], C, n - 1);
            bool c = false;
            if (C >= souvenirs[n] && !a && !b)
                c = SubProblem(souvenirs, A, B, C - souvenirs[n], n - 1);

            return a || b || c;

        }
    }
}
