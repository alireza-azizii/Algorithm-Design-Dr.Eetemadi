using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine(i +" -->> " + ((Fibonacci(i + 2) - 1)%10));
                
            }
        }
        public static string Process(string inStr , Func<long , long > longProcessor)
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }
        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            var toks = inStr.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long a = long.Parse(toks[0]);
            long b = long.Parse(toks[1]);
            return longProcessor(a, b).ToString();
        }
        //1--------------------------------
        //0 1 1 2 3 5 8 13 21 34
        //0 1 2 3 4 5 6 7  8  9
        public static long Fibonacci(long n)
        {

            List<long> fibList = new List<long>((int)n)
            {
                0,
                1
            };
            for (int i = 2; i <= n; i++)
            {
                fibList.Add(fibList[i - 1] + fibList[i - 2]);
            }
            return fibList[(int)n];

        }
        public static string ProcessFibonacci(string inStr) =>
            Process(inStr, Fibonacci);
        //2------------------------------
        public static long Fibonacci_LastDigit(long n)
        {
            List<long> fibList = new List<long>((int)n)
            {
                0,
                1
            };
            for (int i = 2; i <= n; i++)
            {
                fibList.Add(fibList[i - 1]%10 + fibList[i - 2]%10);
            }
            return fibList[(int)n]%10;
        }
        public static string ProcessFibonacci_LastDigit(string inStr) =>
            Process(inStr, Fibonacci_LastDigit);
        //3--------------------------------
        public static long GCD(long a , long b)
        {
            if (a < b)
            {
                a += b;
                b = a - b;
                a = a - b;
            }
            while (b != 0)
            {
                
                a = a % b;
                if (a < b)
                {
                    a += b;
                    b = a - b;
                    a = a - b;
                }
            }

            return a;
        }
        public static string ProcessGCD(string inStr) =>
            Process(inStr, GCD);

        //4--------------------------------
        public static long LMC(long a , long b)
        {

            return (a*b)/GCD(a,b);
        }
        public static string ProcessLMC(string inStr) =>
            Process(inStr, LMC);

        //5---------------------------------
        public static long Fibonacci_Mod(long n, long m)
        {
            List<int> Mod = new List<int>();
            
            int a=0;
            int b=1;
            Mod.Add(a);
            Mod.Add(b);
            while(true)
            {
                a = (Mod[Mod.Count - 1] + Mod[Mod.Count-2] )% (int)m;
                b = (Mod[Mod.Count - 1] + a) % (int)m;
                if (a == 0 && b == 1)
                    break;
                Mod.Add(a);
            }

            n %= Mod.Count();
            return Mod[(int)n];
        }
        public static string ProcessFibonacci_Mod(string inStr) =>
            Process(inStr, Fibonacci_Mod);

        //6-------------------------------------------
        public static long Fibonacci_Sum(long n)
        {
            long d = Fibonacci_Mod(n + 2 ,10) - 1;
            if(d == -1)
                d= 9;
            return d;
        }
        public static string ProcessFibonacci_Sum(string inStr) =>
            Process(inStr, Fibonacci_Sum);

        //7---------------------------------------------
        public static long Fibonacci_Partial_Sum(long n , long m)
        {
            if ( n< m)
            {
                m += n;
                n = m - n;
                m = m - n;
            }
            long s = Fibonacci_Sum(n) - Fibonacci_Sum(m-1) ;
            if (s < 0)
                s += 10;
            return s;
        }
        public static string ProcessFibonacci_Partial_Sum(string inStr) =>
            Process(inStr, Fibonacci_Partial_Sum);


        //8-----------------------------------------------
        public static long Fibonacci_Sum_Squares(long n) =>
            (Fibonacci_Mod(n, 10) * (Fibonacci_Mod(n, 10) + Fibonacci_Mod(n - 1, 10))) % 10;
        public static string ProcessFibonacci_Sum_Squares(string inStr) =>
            Process(inStr, Fibonacci_Sum_Squares);
    }
}
