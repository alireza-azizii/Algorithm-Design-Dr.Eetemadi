using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
        /// <summary>
        /// 1-The goal in this problem is to find the minimum number of coins needed to change the input value
        ///(an integer) into coins with denominations 1, 5, and 10.
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static long ChangingMoney1(long money)
        {
            List<long> minList = new List<long> {0, 1, 2, 3, 4, 1, 2, 3, 4, 5, 1 };
            for (int i = 11; i <= (int)money; i++)
            {
                minList.Add(MinimunOf3Numbers(minList[i - 1], minList[i - 5], minList[i - 10])+1);
            }
            return minList[(int)money];
        }
        public static long MinimunOf3Numbers(long a ,long b, long c)
        {
            if (a <= b && a <= c)
                return a;
            else if (b <= a && b <= c)
                return b;
            else
                return c;
        }
        public static string ProcessChangingMoney1(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>)ChangingMoney1);
        /// <summary>
        /// 2
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="weights"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static long MaximizingLoot2(long capacity , long[] weights,long[]values)
        {
            long c;
            long maxValue = 0;
            for (int i = 0; i < values.Length ; i++)
            {
                for (int j = i+1; j < values.Length; j++)
                {
                    if((values[i] / (double)weights[i]) <(values[j] / (double)weights[j]))
                    {
                        c = values[i];
                        values[i] = values[j];
                        values[j] = c;
                        c = weights[i];
                        weights[i] = weights[j];
                        weights[j] = c;
                    }
                }
            }
            for (int i = 0; capacity>0 ; i++)
            {
                if(capacity>weights[i])
                {
                    capacity -= weights[i];
                    maxValue += values[i];
                }
                else
                {
                    maxValue += (long)((values[i] / (double)weights[i]) * capacity);
                    capacity = 0;
                }
            }
            return maxValue;
        }
        
        public static string ProcessMaximizingLoot2(string inStr) =>
            TestTools.Process(inStr,
                (Func<long, long[], long[], long>)MaximizingLoot2);
        /// <summary>
        /// 3
        /// </summary>
        /// <returns></returns>
        public static long MaximizingOnlineAdRevenue3(long slotCount , 
            long[] adRevenue , long[]averageDailyClick)
        {
            long MaxmizeCost = 0;
            long c;
            for (int i = 0; i < adRevenue.Length; i++)
            {
                for (int j = i; j < adRevenue.Length; j++)
                {
                    if(adRevenue[i] < adRevenue[j])
                    {
                        c = adRevenue[i];
                        adRevenue[i] = adRevenue[j];
                        adRevenue[j] = c;
                    }
                }
            }
            for (int i = 0; i < averageDailyClick.Length; i++)
            {
                for (int j = i; j < averageDailyClick.Length; j++)
                {
                    if (averageDailyClick[i] < averageDailyClick[j])
                    {
                        c = averageDailyClick[i];
                        averageDailyClick[i] = averageDailyClick[j];
                        averageDailyClick[j] = c;
                    }
                }
            }
            for (int i = 0; i < adRevenue.Length; i++)
            {
                MaxmizeCost += (adRevenue[i] * averageDailyClick[i]);
            }
            return MaxmizeCost;
        }
        public static string ProcessMaximizingOnlineAdRevenue3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingOnlineAdRevenue3);
        /// <summary>
        /// 4
        /// </summary>
        /// <param name="tenantCount"></param>
        /// <param name="startTimes"></param>
        /// <param name="endTimes"></param>
        /// <returns></returns>
        public static long CollectingSignatures4(long tenantCount,
            long[] startTimes , long[] endTimes)
        {
            long c;
            for (int i = 0; i < startTimes.Length; i++)
            {
                for (int j = i; j < startTimes.Length; j++)
                {
                    if (endTimes[i] > endTimes[j])
                    {
                        c = startTimes[i];
                        startTimes[i] = startTimes[j];
                        startTimes[j] = c;
                        c = endTimes[i];
                        endTimes[i] = endTimes[j];
                        endTimes[j] = c;
                    }
                }
            }
            long minizeLine = 0;
            bool[] isSign = new bool[startTimes.Length];
            isSign.Select(x => x = false);
            for (int i = 0; i < startTimes.Length; i++)
            {
                if(!isSign[i])
                {
                    minizeLine++;
                    for (int j = 0; j < startTimes.Length; j++)
                    {
                        if(startTimes[j]<= endTimes[i])
                        {
                            isSign[j] = true;
                        }
                    }
                }
            }
            
            return minizeLine;
        }
        public static string ProcessCollectingSignatures4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long>)CollectingSignatures4);

        /// <summary>
        /// 5
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long[] MaximizeNumberOfPrizePlaces5(long n)
        {
            List<long> prizes = new List<long>();
            for(int i=1;i*2<n; i++)
            {
                prizes.Add(i);
                n -= i;
            }
            prizes.Add(n);
            return prizes.ToArray();
        }
        public static string ProcessMaximizeNumberOfPrizePlaces5(string inStr) =>
            TestTools.Process(inStr,(Func<long , long[]>) MaximizeNumberOfPrizePlaces5);

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="n"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static string MaximizeSalary6(long n , long[] numbers)
        {
            long maxOfNumbers = 0;
            string mySalary = "";
            for(int x=0; x<numbers.Length;x++)
            {
                maxOfNumbers = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] != -1)
                        maxOfNumbers = MaxOfPairNumbers(numbers[i], maxOfNumbers);
                }
                mySalary += maxOfNumbers.ToString();
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == maxOfNumbers)
                    {
                        numbers[i] = -1;
                        break;
                    }
                }
            }
                     
            
            return mySalary;
        }
        public static long MaxOfPairNumbers(long a, long b)
        {
            string A = a.ToString();
            string B = b.ToString();
            return int.Parse(A + B) > int.Parse(B + A) ? a : b;
        }
        public static string ProcessMaximizeSalary6(string inStr) =>
            TestTools.Process(inStr, MaximizeSalary6);

    }
}
