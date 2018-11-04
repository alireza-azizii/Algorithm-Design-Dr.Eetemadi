using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public static class Extensions
    {
        public static List<long> Adding(this List<long> a, List<long> b)
        {
            List<long> result = new List<long>(a.Count + b.Count);
            for (int i = 0; i < result.Capacity; i++)
            {
                if (i < a.Count)
                    result.Add(a[i]);
                else
                    result.Add(b[i - a.Count]);
            }
            return result;
        }
        public static void Swap(this long[] input, long a , long b)
        {
            long x = input[a];
            input[a] = input[b];
            input[b] = x;
        }

        public static long[] SubArray(this long[] input , long start  , long end)
        {
            long[] sub = new long[end - start];
            for (long i = 0; i < sub.Length; i++)
            {
                sub[i] = input[i + start];
            }
            return sub;

        }

    }
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
        /// <summary>
        /// 1
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long[] BinarySearch1(long[] a, long[] b)
        {
            long[] resultArray = new long[b.Length];

            for (int i = 0; i < b.Length; i++)
            {
                resultArray[i] = MyBinarySearch(a, 0, a.Length-1, b[i]);
            }
            return resultArray;
        }

        private static long MyBinarySearch(long[] a, long low,long high,long item )
        {
            long mid = (low + high) / 2;
            if (high < low)
                return -1;
            if (item == a[mid])
                return mid;
            else if (item < a[mid])
                return MyBinarySearch(a, low, mid - 1, item);
            else
                return MyBinarySearch(a, mid + 1, high, item);
            
        }
        public static string ProcessBinarySearch1(string inStr) =>
            TestTools.Process(inStr, (Func<long[] , long[] , long[] >)BinarySearch1);

        /// <summary>
        /// 2
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>

        public static long MajorityElement2(long n, long[] a)
        {
            Tuple<List<long>, List<long>> result = GetMagorityElement(a, 0, n);
            if (result.Item1.Count > n / 2)
            {
                return 1;
            }
            else
                return 0;
            
        }

        public static Tuple<List<long> , List<long>> GetMagorityElement(long[] a , long left , long right)
        {
            if (left + 1  == right)
            {
                return Tuple.Create(new List<long>() { a[left] }, new List<long> { });
            }

            long mid = (left+(right - left) / 2);
            Tuple<List<long>, List<long>> leftHalf = GetMagorityElement(a, left, mid);
            Tuple<List<long>, List<long>> rightHalf = GetMagorityElement(a, mid, right);

            return CountMerge(leftHalf, rightHalf);
        }
 
        private static Tuple<List<long>, List<long>> CountMerge(Tuple<List<long>, List<long>> leftHalf, Tuple<List<long>, List<long>> rightHalf)
        {
            List<long> leftMajors, rightMajors, leftMinors, rightMinors;
            Tuple<List<long>, List<long>> a = ChunckProcess(leftHalf.Item1, rightHalf.Item2);
            leftMajors = a.Item1;
            rightMinors = a.Item2;
            Tuple<List<long>, List<long>> b = ChunckProcess(rightHalf.Item1, leftHalf.Item2);
            rightMajors = b.Item1;
            leftMinors = b.Item2;

            if (leftMajors[0] == rightMajors[0])
                return Tuple.Create(leftMajors.Adding(rightMajors), leftMinors.Adding(rightMinors));
            else if (leftMajors.Count > rightMajors.Count)
                return Tuple.Create(leftMajors, rightMajors.Adding(leftMinors).Adding(rightMinors));
            else
                return Tuple.Create(rightMajors, leftMajors.Adding(rightMinors).Adding(leftMinors));
            

        }
        private static Tuple<List<long>, List<long>> ChunckProcess(List<long> majors , List<long> others)
        {
            List<long> left = new List<long>();
            for (int i = 0; i < others.Count; i++)
            {
                if(majors[0] == others[i])
                {
                    majors.Add(others[i]);
                }
                else
                {
                    left.Add(others[i]);
                }
            }
            return new Tuple<List<long>, List<long>>(majors, left);
        }
        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        /// <summary>
        /// 3
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            return MyQuickSort(a, 0, n - 1);
        }
        public static Tuple<long , long> Partions(long[] a , long left , long right)
        {
            long x = a[left], start = left + 1, end = left;

            for (long i = left+1; i < right+1; i++)
            {
                if(a[i] <= x)
                {
                    end++;
                    a.Swap(i, end);
                    if(a[end]<x)
                    {
                        a.Swap(start, end);
                        start++;
                    }
                }
            }
            a.Swap(left, start - 1);

            return Tuple.Create(start, end);
        }
        private static long[] MyQuickSort(long[] a ,long left , long right )
        {

            Random rnd = new Random();
            if (left >= right)
                return a;
            long pivot = rnd.Next((int)left, (int)right);

            a.Swap(left, pivot);

            var T = Partions(a, left, right);

            MyQuickSort(a, left, T.Item1 - 1);
            MyQuickSort(a, T.Item2 + 1, right);

            return a;
        }
        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        /// <summary>
        /// 4
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static long NumberofInversions4(long n, long[] a)
        {
            long[] temp = new long[n];
            return MergeSort(a, temp, 0, n - 1);
        }
        public static long MergeSort(long[] a , long[] temp , long left , long right)
        {
            long mid, inversionsCount = 0;
            if(right > left)
            {
                mid = (right + left) / 2;
                inversionsCount = MergeSort(a, temp, left, mid);
                inversionsCount += MergeSort(a, temp, mid + 1, right);

                inversionsCount += Merge(a, temp, left, mid + 1, right);
            }
            return inversionsCount;
        }

        private static long Merge(long[] a, long[] temp, long left, long mid, long right)
        {
            long i = left, j = mid, k = left;
            long inversionsCount = 0;
            while((i <= mid - 1) && (j <= right))
            {
                if(a[i] <= a[j])
                {
                    temp[k++] = a[i++];
                }
                else
                {
                    temp[k++] = a[j++];
                    inversionsCount += (mid - i);

                }
            }

            while (i <= mid - 1)
                temp[k++] = a[i++];

            while (j <= right)
                temp[k++] = a[j++];

            for (i = left; i <= right; i++)
            {
                a[i] = temp[i];
            }

            return inversionsCount;
        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        /// <summary>
        /// 5
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startSegments"></param>
        /// <param name="endSegment"></param>
        /// <returns></returns>
        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            List<Tuple<long, char>> pairs = new List<Tuple< long, char>>();
            foreach (var item in startSegments)
            {
                pairs.Add(Tuple.Create(item, 'l'));
            }
            foreach (var item in endSegment)
            {
                pairs.Add(Tuple.Create(item, 'r'));
            }
            foreach (var item in points)
            {
                pairs.Add(Tuple.Create(item, 'p'));
            }
            List<Tuple<long, char>> orderdePairs = pairs.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();
            long countLeft = 0;
            Dictionary<long, long> pointsDict = new Dictionary<long, long>();
            for (int i = 0; i < orderdePairs.Count; i++)
            {
                if (orderdePairs[i].Item2 == 'l')
                {
                    countLeft++;
                }
                else if(orderdePairs[i].Item2 == 'r' )
                {
                    countLeft--;
                }
                else
                {
                    if(!pointsDict.ContainsKey(orderdePairs[i].Item1))
                        pointsDict.Add(orderdePairs[i].Item1, countLeft);
                }
            }

            long[] countInPoint = new long[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                countInPoint[i] = pointsDict[points[i]];
            }
            return countInPoint;
        }
        
        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr, (Func<long[] , long[] , long[] ,long[]>)OrganizingLottery5);

        /// <summary>
        /// 6
        /// </summary>
        /// <param name="n"></param>
        /// <param name="xPoints"></param>
        /// <param name="yPoints"></param>
        /// <returns></returns>
        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            List<Tuple<long, long>> points = new List<Tuple<long, long>>((int)n);

            for (int i = 0; i < n; i++)
            {
                points.Add(Tuple.Create(xPoints[i], yPoints[i]));
            }

            List<Tuple<long, long>> sortedByx = points.OrderBy(x => x.Item1).ToList();

            return Math.Round(ClosestPointsI(sortedByx), 4);
        }

        private static double ClosestPointsI(List<Tuple<long, long>> points)
        {       
            if(points.Count <= 3)
                return MinimumDistance(points);

            List<Tuple<long, long>> leftX = points.Take(points.Count / 2).ToList();
            List<Tuple<long, long>> rightX = points.Skip(points.Count / 2).ToList();
            double minInLeft = ClosestPointsI(leftX);
            double minInRight = ClosestPointsI(rightX);

            double minI = minInLeft >= minInRight ? minInRight : minInLeft;

            double middleX = (leftX.Last().Item1 + rightX.First().Item1) / (double)2;

            List<Tuple<long, long>> left = new List<Tuple<long, long>>();
            for (int i = 0; i < leftX.Count; i++)
            {
                if (Math.Abs(leftX[i].Item1 - middleX) <= minI)
                    left.Add(leftX[i]);
            }
            List<Tuple<long, long>> right = new List<Tuple<long, long>>();
            for (int i = 0; i < rightX.Count; i++)
            {
                if (Math.Abs(rightX[i].Item1 - middleX) <= minI)
                    right.Add(rightX[i]);
            }

            List<Tuple<long, long>> total = new List<Tuple<long, long>>();
            total.AddRange(left);
            total.AddRange(right);

            var sortedByY = total.OrderBy(x => x.Item2);

            for (int i = 0; i < total.Count; i++)
            {
                for (int j = i+1; j < Math.Min(i+8 , total.Count); j++)
                {
                    if (Math.Abs(total[i].Item2 - total[j].Item2) < minI)
                        minI = Math.Min(minI , Distance(total[i] , total[j]));
                }
            }
            return minI;
        }

        private static double MinimumDistance(List<Tuple<long, long>> points)
        {
            double result = double.MaxValue;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i+1; j < points.Count; j++)
                {     
                    result = Math.Min(result , Distance(points[i] , points[j]));
                }
            }
            return result;
        }

        private static double Distance(Tuple<long, long> p1, Tuple<long, long> p2)
        {
            return (Math.Pow((Math.Pow(p1.Item1 - p2.Item1, 2) + Math.Pow(p1.Item2 - p2.Item2, 2)), 0.5));
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
