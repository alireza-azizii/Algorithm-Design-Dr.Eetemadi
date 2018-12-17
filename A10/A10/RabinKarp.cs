using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class RabinKarp : Processor
    {
        public RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);

        public long[] Solve(string pattern, string text)
        {
            List<long> occurrences = new List<long>();

            long[] textHashes = PreComputeHashes(text , pattern.Length, BigPrimeNumber , 1 );

            long patternHash = PolyHash(pattern, 1);

            for (int i = 0; i < text.Length - pattern.Length+1; i++)
            {
                if(patternHash != textHashes[i])
                {
                    continue;
                }
                if(pattern.Equals(text.Substring(i, pattern.Length)))
                {
                    occurrences.Add(i);
                }
            }
            return occurrences.ToArray();
        }

        
        public const long BigPrimeNumber = 1000000007;

        public static long[] PreComputeHashes(
            string T, 
            int P, 
            long p, 
            long x)
        {
            long[] ComputedHashes = new long[T.Length - P + 1];
            string S = T.Substring(T.Length - P, P);
            ComputedHashes[T.Length - P] = PolyHash(S , x);
            long h = 1;
            for (int i = 1; i < P + 1; i++)
                h = (h * x) % p;
            for (int i = T.Length - P - 1; i >= 0; i--)
            {
                ComputedHashes[i] = (x * ComputedHashes[i + 1] + T[i] - h * T[i + P]) % p;
            }
            return ComputedHashes;
        }

        public static long PolyHash(string str, long ChosenX)
        {
            long hash = 0;

            for (int i = str.Length - 1; i >= 0; i--)
            {
                hash = ((hash * ChosenX + str[i]) % BigPrimeNumber + BigPrimeNumber) % BigPrimeNumber;
            }

            return hash;
        }
    }
}
