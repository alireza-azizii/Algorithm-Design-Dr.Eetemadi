using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximizingArithmeticExpression : Processor
    {
        public MaximizingArithmeticExpression(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string expression)
        {
            long[] numbers = expression.Split(new char[] { '+', '-', '*', '/' }).Select(x => long.Parse(x)).ToArray();

            char[] operators = new char[expression.Length - numbers.Length];

            for (int i = 0,j=0; i < expression.Length; i++)
            {
                if(!Char.IsDigit(expression[i]))
                {
                    operators[j] = expression[i];
                    j++;
                }
            }

            long[,] maximunResult = new long[numbers.Length, numbers.Length];

            long[,] minimunResult = new long[numbers.Length, numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                minimunResult[i , i] = numbers[i];
                maximunResult[i, i] = numbers[i];
            }

            long a,b,c,d , min , max ;

            for (int s = 0 ; s < numbers.Length ; s++)
            {
                for (int i = 0 , j; i < numbers.Length-s-1; i++)
                {

                    j = i + s + 1;
                    max = long.MinValue;
                    min = long.MaxValue;

                    for (int k = i; k < j; k++)
                    {
                        a = Calculator(maximunResult[i, k], maximunResult[k + 1, j], operators[k]);
                        b = Calculator(maximunResult[i, k], minimunResult[k + 1, j], operators[k]);
                        c = Calculator(minimunResult[i, k], maximunResult[k + 1, j], operators[k]);
                        d = Calculator(minimunResult[i, k], minimunResult[k + 1, j], operators[k]);

                        min = Math.Min(min, Math.Min(a, Math.Min(b, Math.Min(c, d))));
                        max = Math.Max(max, Math.Max(a, Math.Max(b, Math.Max(c, d))));
                    }

                    maximunResult[i, j] = max;
                    minimunResult[i, j] = min;
                }
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length; j++)
                {
                    Console.Write($"{maximunResult[i,j]} ");
                }
                Console.Write("    ");
                for (int j = 0; j < numbers.Length; j++)
                {
                    Console.Write($"{minimunResult[i, j]} ");
                }
                Console.Write("\n");
            }
            return maximunResult[0 , numbers.Length-1];
        }

        private long Calculator(long a , long b ,char operate)
        {
            switch(operate)
            {
                case '*':
                    return a * b;
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '/':
                    return a / b;
            }
            return 0;
        }
    }
}
