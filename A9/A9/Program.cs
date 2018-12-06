using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A9
{
    public class Program
    {
        static void Main(string[] args)
        {
            MergingTables m =new MergingTables("");
            m.Solve(new long[] { 1,1,1,1,1}, new long[] {3, 2,1,5,5 }, new long[] {5,4,4,4,3 });
        }
    }
}
