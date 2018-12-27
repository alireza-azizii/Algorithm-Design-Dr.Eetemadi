using System;
using System.Collections;
using System.Linq;

namespace E2
{
    public class Q3BloomFilter
    {
        public BitArray Filter;

        public Func<string, int>[] HashFunctions;

        public Q3BloomFilter(int filterSize, int hashFnCount)
        {
            // زحمت بکشید پیاده سازی کنید
            Random rnd = new Random();
            ChosenX = new int[hashFnCount];
            for (int i = 0; i < hashFnCount; i++)
            {
                ChosenX[i] = rnd.Next( 0,int.MaxValue);
            }
            Filter = new BitArray(filterSize);

            //HashFunctions = new Func<string, int>[hashFnCount];
            //for (int i = 0; i < HashFunctions.Length; i++)
            //{
            //    HashFunctions[i] = (str => MyHashFunction(str, i));
            //}

        }
        public static int BigPrimeNumber = 1000000007;
        public int[] ChosenX;

        public int MyHashFunction(string str, int num)
        {
            long hash = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                hash = (hash * num + str[i]) % BigPrimeNumber;
            }
            var result = (hash % Filter.Length);
            return (int)result;
        }

        public void Add(string str)
        {

            foreach (var item in ChosenX)
            {
                Filter[MyHashFunction(str, item)] = true;
            }
            // زحمت بکشید پیاده سازی کنید
        }

        public bool Test(string str)
        {
            foreach (var item in ChosenX)
            {
                if(!Filter[MyHashFunction(str,item)])
                {
                    return false;
                }
            }
            return true;
            // زحمت بکشید پیاده سازی کنید
        }
    }
}