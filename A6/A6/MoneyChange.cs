using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public MoneyChange(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>) Solve);

        public long Solve(long n)
        {
            if(n == 34)
                Console.WriteLine();
            List<long> MinimunMoney = new List<long>() { 0,1, 2, 1, 1, 2, 2, 2, 2 };
            long minimum;
            for (int i = 9; i <= n ; i++)
            {
                minimum = long.MaxValue;
                foreach (int coin in COINS)
                {
                    if( MinimunMoney[i-coin] < minimum)
                    {
                        minimum = MinimunMoney[i - coin];
                    }
                }
                MinimunMoney.Add(minimum + 1);
            }

            return MinimunMoney[(int)n];
        }
        
        
    }
}
