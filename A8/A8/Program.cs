using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8
{
    public class Program
    {
        static void Main(string[] args)
        {
            TreeHeight t = new TreeHeight("");
            Console.WriteLine(t.Solve(5, new long[] { 4, -1, 4, 1, 1 })); 
        }
       
        
    }
}
