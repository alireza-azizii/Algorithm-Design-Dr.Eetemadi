using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    public class Program
    {
        static void Main()
        {
            LCSOfTwo lcf = new LCSOfTwo("TD4");
            lcf.Solve(new long[] { 2, 7 ,5 }, new long[] {2,5 });
        }
    }
}
