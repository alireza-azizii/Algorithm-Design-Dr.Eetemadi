using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class Program
    {
        static void Main(string[] args)
        {
            IsItBSTHard b = new IsItBSTHard("");
            List<long[]> input = new List<long[]>();
            while (true)
            {
                long[] line = Console.ReadLine().Split().Select(x => long.Parse(x)).ToArray();
                if (line[0] == -1)
                    break;
                input.Add(line);
            }

            var d = b.Solve(input.ToArray());
        }
    }
}
