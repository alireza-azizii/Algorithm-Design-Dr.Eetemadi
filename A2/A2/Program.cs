using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Program
    {
        static void Main(string[] args)
        {
        }
        public static int NaiveMaxPairwiseProduct(List<int> numbers)
        {
            int product = 0;
            int multi = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    multi = numbers[i] * numbers[j];
                    if (multi > product)
                        product = multi;
                }
            }
            return product;
        }
        public static string Process(string input)
        {
            var inData = input.Split(new char[] { '\n', '\r', ' ' }
            , StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.Parse(s))
            .ToList();
            return FastMaxPairwiseProduct(inData).ToString();
        }

        public static int FastMaxPairwiseProduct(List<int> numbers)
        {
            int index1 = 0;
            int index2;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[index1] < numbers[i])
                {
                    index1 = i;

                }
            }

            index2 = index1 == 0 ? 1 : 0;
            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[index2] < numbers[i] && i != index1)
                {
                    index2 = i;
                }
            }

            return numbers[index1] * numbers[index2];
        }
    }
}
